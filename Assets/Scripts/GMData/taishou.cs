using Newtonsoft.Json;
using System.Collections;
using System.Linq;

using UnityEngine;

namespace TaisEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Taishou
    {
        [JsonProperty]
        public string name;

        [JsonProperty]
        public int age;

        [JsonProperty]
        public double prestige;

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
        }

        internal Taishou()
        {

        }

        [JsonProperty]
        private string _background;
    }
}
