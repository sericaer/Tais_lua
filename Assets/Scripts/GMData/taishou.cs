using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class Taishou
    {
        [JsonProperty]
        public string name;

        [JsonProperty]
        public int age;

        [JsonProperty]
        public double prestige;

        [JsonProperty]
        public List<Buffer> buffers;

        public BackgroundDef.Interface background
        {
            get
            {
                return BackgroundDef.Enumerate().Single(x=>x.name == _background);
            }
        }

        public Taishou(string name, int age, string background)
        {
            this.name = name;
            this.age = age;
            this._background = background;

            this.buffers = new List<Buffer>();
            GMData.inst.allBuffers.Add(this.buffers);
        }

        internal Taishou()
        {

        }

        [JsonProperty]
        private string _background;
    }
}
