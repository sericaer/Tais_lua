using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using XLua;

namespace TaisEngine
{

    //[JsonConverter(typeof(PopConverter))]
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class Pop
    {
        //internal Family family;
        [JsonProperty]
        public string name;

        [JsonProperty]
        public string depart_name;

        [JsonProperty]
        public double num;

        [JsonProperty]
        public List<Buffer> buffers = new List<Buffer>();

        [JsonProperty]
        public string family_name;

        internal string key
        {
            get
            {
                return $"{depart_name}|{name}";
            }
        }

        public PopDef.Interface def
        {
            get
            {
                return PopDef.Find(name);
            }
        }

        public Depart depart
        {
            get
            {
                return GMData.inst.departs.Single(x => x.name == depart_name);
            }
        }

        public bool is_consume
        {
            get
            {
                return def.consume != null;
            }
        }

        public double? consume
        {
            get
            {
                if(consumeDetail == null)
                {
                    return null;
                }

                return consumeDetail.Sum(x => x.value);
            }
        }

        public Family family
        {
            get
            {
                if(family_name == "")
                {
                    return null;
                }

                return GMData.inst.families.Single(x => x.name == family_name);
            }
        }

        internal IEnumerable<(string name, double value)> consumeDetail
        {
            get
            {
                if (!is_consume)
                {
                    return null;
                }

                List<(string name, double value)> rslt = new List<(string name, double value)>();
                rslt.Add(("BASE_VALUE", def.consume.Value));
                rslt.AddRange(depart.buffers.exist_consume_effects().Select(x => (x.name, x.value * def.consume.Value)));

                return rslt;
            }
        }


        internal Pop(PopDef.Interface popDef, string depart, double num)
        {
            this.name = popDef.name;
            this.depart_name = depart;
            this.num = num;
            this.family_name = "";

            foreach (var elem in BufferDef.BufferPopDef.Enumerate())
            {
                buffers.Add(new Buffer(elem));
            }

            if(def.is_family)
            {
                var family = Family.Generate(BackgroundDef.Enumerate().OrderBy(x => Guid.NewGuid()).First().name);
                this.family_name = family.name;
            }

            //this.def.mod.AddBuffersPyObj(this.def, buffers);

            GMData.inst.pops.Add(this);
        }

        internal Pop()
        {

        }
        //internal double getExpectTax(int level)
        //{
        //    return def.num * 0.001;
        //}
    }

    //public class PopConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return typeof(Pop) == objectType;
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var pop = value as Pop;

    //        var popJObject = new JObject();
    //        popJObject.Add("name", pop.name);
    //        popJObject.Add("num", pop.num);

    //        popJObject.WriteTo(writer);
    //    }
    //}
}
