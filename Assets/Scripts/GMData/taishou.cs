using Newtonsoft.Json;
using System.Collections;
using System.Linq;

using UnityEngine;

namespace TaisEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class Taishou
    {
        [JsonProperty]
        internal string name;

        [JsonProperty]
        internal int age;

        internal BackgroundDef.Interface background
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

        [JsonProperty]
        private string _background;
    }
}
