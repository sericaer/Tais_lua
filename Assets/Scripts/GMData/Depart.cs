using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Tools;
using UnityEngine;

namespace TaisEngine
{
    public class Depart
    {
        public static int growingdays = 240;

        //internal static IEnumerable<GEvent> DaysInc()
        //{
        //    foreach(var depart in GMData.inst.listDepart)
        //    {
        //        if (depart.cropGrowing != null)
        //        {
        //            depart.cropGrowing += depart.growSpeedDetail.Sum(x => x.value);
        //        }

        //        if (GMData.inst.date.month == 1 && GMData.inst.date.day == 1)
        //        {
        //            depart.growStart();
        //        }

        //        if (GMData.inst.date.month == 8 && GMData.inst.date.day == 1)
        //        {
        //            depart.growFinish();
        //        }
        //    }

        //    yield break;
        //}

        public List<Pop> pops = new List<Pop>();

        //public List<Buffer> buffers = new List<Buffer>();

        internal string color
        {
            get
            {
                return string.Format("({0:0}, {1:0}, {2:0})", def.color[0], def.color[1], def.color[2]);
            }
        }

        internal DepartDef def;

        //internal double? cropGrowing;

        //internal List<(string name, double value)> growSpeedDetail
        //{
        //    get
        //    {
        //        var rslt = new List<(string name, double value)>();

        //        rslt.Add(("BASE_VALUE", 100.0/ growingdays));
        //        rslt.AddRange(buffers.Where(x => x.def.cropGrowingEffect != null).Select(x => (x.key, x.def.cropGrowingEffect() * 100.0 / growingdays)));

        //        return rslt;
        //    }
        //}

        internal Depart(DepartDef def)
        {
            this.def = def;

            foreach (var popdef in def.pops.Values)
            {

                pops.Add(new Pop(popdef, this));
            }

            //this.def.mod.AddBuffersPyObj(this.def, buffers);
        }

        //internal void growStart()
        //{
        //    cropGrowing = 0;
        //}

        //internal void growFinish()
        //{
        //    cropGrowing = null;
        //}
    }
}
