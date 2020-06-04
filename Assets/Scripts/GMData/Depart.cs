﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Tools;
using UnityEngine;
using XLua;

namespace TaisEngine
{

    //[JsonConverter(typeof(DepartConverter))]
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class Depart
    {
        public static int growingdays = 250;

        internal static IEnumerable<EventDef.Interface> DaysInc()
        {
            foreach(var depart in GMData.inst.departs)
            {
                depart.cropGrowingProcess();
            }

            yield break;
        }

        internal IEnumerable<Pop> pops
        {
            get
            {
                return GMData.inst.pops.Where(x=>x.depart == name);
            }
        }


        //public List<Buffer> buffers = new List<Buffer>();

        internal string color
        {
            get
            {
                return string.Format("({0:0}, {1:0}, {2:0})", def.color[0], def.color[1], def.color[2]);
            }
        }

        [JsonProperty]
        public string name;

        [JsonProperty]
        public double crop_growing_percent;

        [JsonProperty]
        public BufferManager buffers;

        internal double cropGrowingSpeed
        {
            get
            {
                return growSpeedDetail.Sum(x => x.value);
            }
        }


        internal DepartDef.Interface def
        {
            get
            {
                return DepartDef.Find(name);
            }
        }

        internal List<(string name, double value)> growSpeedDetail
        {
            get
            {
                var rslt = new List<(string name, double value)>();

                rslt.Add(("BASE_VALUE", 100.0 / growingdays));
                rslt.AddRange(buffers.Where(x => x.exist && x.def.crop_growing_effect != null)
                                     .Select(x => (x.name, x.def.crop_growing_effect() * 100.0 / growingdays)));

                return rslt;
            }
        }

        internal Depart(DepartDef.Interface def)
        {
            this.name = def.name;
            this.buffers = new BufferManager();

            foreach (var elem in def.pop_init)
            {
                new Pop(PopDef.Find(elem.Key), this.name, elem.Value);
            }

            foreach (var elem in BufferDef.BufferDepartDef.Enumerate())
            {
                buffers.Add(new Buffer(elem));
            }

            //this.def.mod.AddBuffersPyObj(this.def, buffers);
        }

        internal Depart()
        {

        }

        private void cropGrowingProcess()
        {
            if(GMData.inst.isCropGrowing)
            {
                crop_growing_percent += cropGrowingSpeed;
            }
            else
            {
                crop_growing_percent = 0;
            }

            if(crop_growing_percent < 0)
            {
                crop_growing_percent = 0;
            }
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
