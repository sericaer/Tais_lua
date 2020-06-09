﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace TaisEngine
{
    public class EXPECT_TAX_ROOT
    {
        internal List<(EXPECT_TAX taxed, int days)> histroy = new List<(EXPECT_TAX taxed, int days)>();
        internal EXPECT_TAX current;

        public double expect(double rate)
        {
            var exp = getExpectRoot(rate);
            return exp.value;
        }

        public void start(double rate)
        {
            current = getExpectRoot(rate);
        }

        public double value
        {
            get
            {
                return current.value;
            }
        }

        public void finish()
        {
            current.onFinish();
            histroy.Add((current, GMData.inst.days));

            GMData.inst.economy += current.value;

            current = null;
        }

        internal static EXPECT_TAX getExpectRoot(double rate)
        {
            var expect = new EXPECT_BRANCH("POP_TAX", null);

            foreach (var depart in GMData.inst.departs)
            {
                var expectDepart = new EXPECT_BRANCH(depart.def.name, null);

                foreach (var pop in depart.pops.Where(x => x.def.is_tax))
                {
                    var expectPop = new EXPECT_LEAF(pop.key, pop.num * rate, null);
                    expectPop.getBuffs = () => pop.buffers.exist_tax_effects();

                    expectDepart.children.Add(expectPop);
                }

                expectDepart.getBuffs = () => depart.buffers.exist_tax_effects();

                expect.children.Add(expectDepart);
            }

            return expect;
        }
    }

    abstract class EXPECT_TAX
    {

        internal abstract IEnumerable<(string name, double value)> GetTaxInfo();

        internal virtual IEnumerable<(string name, double value, double effect)> GetBufferInfo()
        {
            return buffs.Select(x => (x.name, x.value, x.value * basevalue));
        }

        internal string name;
        internal double value { get => basevalue * buffEffect; }
        internal double buffEffect
        {
            get
            {
                return buffs.Select(x => x.value + 1).Aggregate(1, (double m, double n) => m * n);
            }
        }

        internal IEnumerable<(string name, double value)> buffs
        {
            get
            {
                if (_buffs != null)
                {
                    return _buffs;
                }

                if (getBuffs != null)
                {
                    return getBuffs();
                }

                return new List<(string name, double value)>();
            }
        }

        internal EXPECT_TAX(string name, Func<List<(string name, double value)>> getBuffs)
        {
            this.name = name;
            this.getBuffs = getBuffs;
        }

        internal virtual void onFinish()
        {
            if (getBuffs != null)
            {
                _buffs = getBuffs().ToList();
            }
        }

        protected virtual double basevalue { get; }

        internal Func<IEnumerable<(string name, double value)>> getBuffs;
        internal List<(string name, double value)> _buffs;
    }

    class EXPECT_LEAF : EXPECT_TAX
    {
        internal EXPECT_LEAF(string name, double baseValue, Func<List<(string name, double value)>> getBuffs) : base(name, getBuffs)
        {
            _basevalue = baseValue;
        }

        protected override double basevalue { get => _basevalue; }

        private double _basevalue;

        internal override IEnumerable<(string name, double value)> GetTaxInfo()
        {
            return new List<(string name, double value)> { (name, value) };
        }
    }

    class EXPECT_BRANCH : EXPECT_TAX
    {
        internal List<EXPECT_TAX> children = new List<EXPECT_TAX>();

        internal EXPECT_BRANCH(string name, Func<List<(string name, double value)>> getBuffs) : base(name, getBuffs)
        {
        }

        internal override IEnumerable<(string name, double value, double effect)> GetBufferInfo()
        {
            var rslt = base.GetBufferInfo().ToList();
            rslt.AddRange(children.SelectMany(x => x.GetBufferInfo()));

            return rslt;
        }

        protected override double basevalue { get => children.Sum(x => x.value); }

        internal override void onFinish()
        {
            base.onFinish();

            foreach (var elem in children)
            {
                elem.onFinish();
            }
        }

        internal override IEnumerable<(string name, double value)> GetTaxInfo()
        {
            return children.SelectMany(x => x.GetTaxInfo().Select(y=>(y.name, y.value * buffEffect)));
        }
    }
}
