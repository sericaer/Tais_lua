using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    public class Buffer
    {
        public string name;
        public bool exist { get; set; }

        internal BufferDef.Interface def
        {
            get
            {
                return BufferDef.BufferDepartDef.Find(name);
            }
        }

        internal Buffer(BufferDef.Interface def)
        {
            this.name = def.name;
        }
    }

    [LuaCallCSharp]
    [JsonConverter(typeof(BufferManagerConverter))]
    public class BufferManager : IEnumerable<Buffer>
    {
        public Buffer find(string name)
        {
            return list.SingleOrDefault(x=>x.name == name);
        }

        internal void Add(Buffer buffer)
        {
            list.Add(buffer);
        }

        public IEnumerator<Buffer> GetEnumerator()
        {
            return ((IEnumerable<Buffer>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Buffer>)list).GetEnumerator();
        }

        private List<Buffer> list = new List<Buffer>();
    }

    internal class BufferManagerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(BufferManager) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var buffers = new BufferManager();

            var jArray = serializer.Deserialize<JArray>(reader);
            foreach(var jobj in jArray)
            {

                buffers.Add(jobj.ToObject<Buffer>()); 
            }

            return buffers;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var buffers = value as BufferManager;

            var jArray = new JArray();
            foreach (var buff in buffers)
            {
                jArray.Add(JToken.FromObject(buff));
            }

            jArray.WriteTo(writer);
        }
    }
}
