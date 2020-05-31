using System;
using System.Collections.Generic;
using System.Linq;

namespace TaisEngine
{
    public class Pop
    { 
        //internal Family family;

        internal PopDef def;

        //internal List<Buffer> buffers = new List<Buffer>();

        internal string key
        {
            get
            {
                return $"{depart.def.name}|{def.name}";
            }
        }

        //internal double? consume
        //{
        //    get
        //    {
        //        if(consumeDetail == null)
        //        {
        //            return null;
        //        }

        //        return consumeDetail.Sum(x => x.value);
        //    }
        //}

        //internal IEnumerable<(string name, double value)> consumeDetail
        //{
        //    get
        //    {
        //        if(def.consume == null)
        //        {
        //            return null;
        //        }

        //        List<(string name, double value)> rslt = new List<(string name, double value)>();
        //        rslt.Add(("BASE_VALUE", def.consume.Value));
        //        rslt.AddRange(depart.buffers.Where(x => x.def.consumeEffect != null).Select(x => (x.key, x.def.consumeEffect() * def.consume.Value)));

        //        return rslt;
        //    }
        //}

        internal Depart depart;

        internal Pop(PopDef def, Depart depart)
        {
            this.def = def;

            this.depart = depart;

            //if(def.with_family)
            //{
            //    family = Family.Generate(BackgroundDef.Enumerate().OrderBy(x => Guid.NewGuid()).First().key);
            //}

            //this.def.mod.AddBuffersPyObj(this.def, buffers);

            GMData.inst.listPop.Add(this);
        }

        internal double getExpectTax(int level)
        {
            return def.num * 0.001;
        }
    }
}
