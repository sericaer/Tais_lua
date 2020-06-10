using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaisEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TAX_INFO
    {
        internal string name;
        internal double value
        {
            get
            {
                if(list == null)
                {
                    return _value;
                }

                return list.Sum(x => x.value);
            }
        }

        internal List<TAX_INFO> list;
        internal double _value;

        internal TAX_INFO()
        {

        }

        internal TAX_INFO(double rate)
        {
            this.name = "POP_TAX";
            Update(rate);
        }

        internal List<(string name, double value)> GetBufferInfo()
        {
            return GetBufferInfo(this);
        }

        private List<(string name, double value)> GetBufferInfo(TAX_INFO curr)
        {
            var rslt = new List<(string name, double value)>();
            if (curr.name.StartsWith("BUFFER:"))
            {
                rslt.Add((name, value));
                return rslt;
            }

            foreach(var elem in list)
            {
                rslt.AddRange(GetBufferInfo(elem));
            }
            return rslt;
        }

        internal List<(string name, double value)> GetTaxInfo()
        {
            return GetTaxInfo(this);
        }

        private List<(string name, double value)> GetTaxInfo(TAX_INFO curr)
        {
            var rslt = new List<(string name, double value)>();
            if (curr.list == null)
            {
                rslt.Add((name, value));
                return rslt;
            }

            foreach (var elem in list)
            {
                rslt.AddRange(GetTaxInfo(elem));
            }
            return rslt;
        }

        internal void Update(double rate)
        {
            var newList = new List<TAX_INFO>();
            foreach (var depart in GMData.inst.departs)
            {
                var departTax = new TAX_INFO() { name = depart.name, list = new List<TAX_INFO>() };

                var departBaseValue = 0.0;
                foreach (var pop in depart.pops.Where(x => x.def.is_tax))
                {
                    var popTax = new TAX_INFO() { name = pop.name, list = new List<TAX_INFO>() };

                    var baseValue = pop.histroy_rec.Last().histroy.num * rate;
                    departBaseValue += baseValue;

                    popTax.list.Add(new TAX_INFO() { name = "base_value", _value = baseValue });

                    var pop_effects = pop.buffers.exist_tax_effects().Select(x => new TAX_INFO() { name = "BUFFER:"+x.name, _value = x.value * baseValue });
                    popTax.list.AddRange(pop_effects);

                    departTax.list.Add(popTax);
                }

                var depart_effects = depart.buffers.exist_tax_effects().Select(x => new TAX_INFO() { name = "BUFFER:"+x.name, _value = x.value * departBaseValue });
                departTax.list.AddRange(depart_effects);

                newList.Add(departTax);
            }

            this.list = newList;
        }
    }


    //[JsonObject(MemberSerialization.OptIn)]
    //public class EXPECT_TAX_ROOT
    //{
    //    [JsonProperty]
    //    internal List<(EXPECT_BRANCH taxed, int days)> histroy = new List<(EXPECT_BRANCH taxed, int days)>();

    //    [JsonProperty]
    //    internal EXPECT_BRANCH current;

    //    public double expect(double rate)
    //    {
    //        var exp = getExpectRoot(rate);
    //        return exp.value;
    //    }

    //    public void start(double rate)
    //    {
    //        current = getExpectRoot(rate);
    //    }

    //    public double value
    //    {
    //        get
    //        {
    //            return current.value;
    //        }
    //    }

    //    public void finish()
    //    {
    //        current.onFinish();
    //        histroy.Add((current, GMData.inst.days));

    //        GMData.inst.economy += current.value;

    //        current = null;
    //    }

    //    internal static EXPECT_BRANCH getExpectRoot(double rate)
    //    {
    //        var expect = new EXPECT_BRANCH("POP_TAX", null);

    //        foreach (var depart in GMData.inst.departs)
    //        {
    //            var expectDepart = new EXPECT_BRANCH(depart.def.name, null);

    //            foreach (var pop in depart.pops.Where(x => x.def.is_tax))
    //            {
    //                var expectPop = new EXPECT_LEAF(pop.key, pop.num * rate, null);
    //                expectPop.getBuffs = () => pop.buffers.exist_tax_effects();

    //                expectDepart.children.Add(expectPop);
    //            }

    //            expectDepart.getBuffs = () => depart.buffers.exist_tax_effects();

    //            expect.children.Add(expectDepart);
    //        }

    //        return expect;
    //    }
    //}

    //[JsonObject(MemberSerialization.OptIn)]
    //abstract class EXPECT_TAX
    //{

    //    internal abstract IEnumerable<(string name, double value)> GetTaxInfo();

    //    internal virtual IEnumerable<(string name, double value, double effect)> GetBufferInfo()
    //    {
    //        return buffs.Select(x => (x.name, x.value, x.value * basevalue));
    //    }

    //    [JsonProperty]
    //    internal string name;

    //    internal double value { get => basevalue * buffEffect; }
    //    internal double buffEffect
    //    {
    //        get
    //        {
    //            return buffs.Select(x => x.value + 1).Aggregate(1, (double m, double n) => m * n);
    //        }
    //    }

    //    internal IEnumerable<(string name, double value)> buffs
    //    {
    //        get
    //        {
    //            if (_buffs != null)
    //            {
    //                return _buffs;
    //            }

    //            if (getBuffs != null)
    //            {
    //                return getBuffs();
    //            }

    //            return new List<(string name, double value)>();
    //        }
    //    }

    //    public EXPECT_TAX()
    //    {

    //    }

    //    internal EXPECT_TAX(string name, Func<List<(string name, double value)>> getBuffs)
    //    {
    //        this.name = name;
    //        this.getBuffs = getBuffs;
    //    }

    //    internal virtual void onFinish()
    //    {
    //        if (getBuffs != null)
    //        {
    //            _buffs = getBuffs().ToList();
    //        }
    //    }

    //    public virtual double basevalue { get; }

    //    internal Func<IEnumerable<(string name, double value)>> getBuffs;
    //    internal List<(string name, double value)> _buffs;
    //}

    //[JsonObject(MemberSerialization.OptIn)]
    //[JsonConverter(typeof(EXPECT_LEAF))]
    //class EXPECT_LEAF : EXPECT_TAX
    //{
    //    public EXPECT_LEAF()
    //    {

    //    }

    //    internal EXPECT_LEAF(string name, double baseValue, Func<List<(string name, double value)>> getBuffs) : base(name, getBuffs)
    //    {
    //        _basevalue = baseValue;
    //    }

    //    public override double basevalue { get => _basevalue; }

    //    [JsonProperty]
    //    private double _basevalue;

    //    internal override IEnumerable<(string name, double value)> GetTaxInfo()
    //    {
    //        return new List<(string name, double value)> { (name, value) };
    //    }
    //}

    //[JsonObject(MemberSerialization.OptIn)]
    //class EXPECT_BRANCH : EXPECT_TAX
    //{
    //    public EXPECT_BRANCH()
    //    {

    //    }

    //    [JsonProperty]
    //    internal List<EXPECT_TAX> children = new List<EXPECT_TAX>();

    //    internal EXPECT_BRANCH(string name, Func<List<(string name, double value)>> getBuffs) : base(name, getBuffs)
    //    {
    //    }

    //    internal override IEnumerable<(string name, double value, double effect)> GetBufferInfo()
    //    {
    //        var rslt = base.GetBufferInfo().ToList();
    //        rslt.AddRange(children.SelectMany(x => x.GetBufferInfo()));

    //        return rslt;
    //    }

    //    public override double basevalue { get => children.Sum(x => x.value); }

    //    internal override void onFinish()
    //    {
    //        base.onFinish();

    //        foreach (var elem in children)
    //        {
    //            elem.onFinish();
    //        }
    //    }

    //    internal override IEnumerable<(string name, double value)> GetTaxInfo()
    //    {
    //        return children.SelectMany(x => x.GetTaxInfo().Select(y=>(y.name, y.value * buffEffect)));
    //    }
    //}

    //public class EXPECT_LEAF_Converter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return typeof(EXPECT_LEAF) == objectType;
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var leaf = value as EXPECT_LEAF;

    //        var popJObject = new JObject();
    //        popJObject.Add("name", pop.name);
    //        popJObject.Add("num", pop.num);

    //        popJObject.WriteTo(writer);
    //    }
    //}
}
