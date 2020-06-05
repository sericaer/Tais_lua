using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaisEngine
{

    //[JsonConverter(typeof(PopConverter))]
    [JsonObject(MemberSerialization.OptIn)]
    public class Pop
    {
        //internal Family family;
        [JsonProperty]
        internal string name;

        [JsonProperty]
        internal string depart_name;

        [JsonProperty]
        internal double num;

        [JsonProperty]
        BufferManager buffers = new BufferManager();

        internal string key
        {
            get
            {
                return $"{depart_name}|{name}";
            }
        }

        internal PopDef.Interface def
        {
            get
            {
                return PopDef.Find(name);
            }
        }

        internal Depart depart
        {
            get
            {
                return GMData.inst.departs.Single(x => x.name == depart_name);
            }
        }

        internal double? consume
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

        internal IEnumerable<(string name, double value)> consumeDetail
        {
            get
            {
                if(def.consume == null)
                {
                    return null;
                }

                List<(string name, double value)> rslt = new List<(string name, double value)>();
                rslt.Add(("BASE_VALUE", def.consume.Value));
                rslt.AddRange(depart.buffers.Where(x => x.def.consume_effect != null).Select(x => (x.name, x.def.consume_effect() * def.consume.Value)));

                return rslt;
            }
        }


        internal Pop(PopDef.Interface popDef, string depart, double num)
        {
            this.name = popDef.name;
            this.depart_name = depart;
            this.num = num;

            //if(def.with_family)
            //{
            //    family = Family.Generate(BackgroundDef.Enumerate().OrderBy(x => Guid.NewGuid()).First().key);
            //}

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
