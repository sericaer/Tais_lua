using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Tools;
using UnityEngine;

namespace TaisEngine
{

    //[JsonConverter(typeof(DepartConverter))]
    [JsonObject(MemberSerialization.OptIn)]
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
        internal List<Pop> pops = new List<Pop>();

        //public List<Buffer> buffers = new List<Buffer>();

        internal string color
        {
            get
            {
                return string.Format("({0:0}, {1:0}, {2:0})", def.color[0], def.color[1], def.color[2]);
            }
        }

        [JsonProperty]
        internal string name;

        internal DepartDef.Interface def
        {
            get
            {
                return DepartDef.Find(name);
            }
        }

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

        internal Depart(DepartDef.Interface def)
        {
            this.name = def.name;

            foreach (var elem in def.pop_init)
            {
                pops.Add(new Pop(PopDef.Find(elem.Key), this.name, elem.Value));
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

    //public class DepartConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return typeof(Depart) == objectType;
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var depart = value as Depart;

    //        var departJObject = new JObject();
    //        departJObject.Add("name", depart.def.name);

    //        var popsJObject = new JArray();
    //        foreach (var pop in depart.pops)
    //        {
    //            popsJObject.Add(JToken.FromObject(pop));
    //        }
    //        departJObject.Add("pops", popsJObject);

    //        departJObject.WriteTo(writer);
    //    }
    //}
}
