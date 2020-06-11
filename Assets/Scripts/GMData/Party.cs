using System;
using Newtonsoft.Json;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class Party
    {
        public Party()
        {

        }

        public BackgroundDef.Interface background
        {
            get
            {
                return BackgroundDef.Find(_background);
            }
        }

        [JsonProperty]
        internal string _background;

        [JsonProperty]
        public double prestige;
    }
}
