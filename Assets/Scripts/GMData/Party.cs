using System;
using Newtonsoft.Json;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class Party
    {
        public Party(string background)
        {
            _background = background;
        }

        public BackgroundDef.Interface background
        {
            get
            {
                return BackgroundDef.Find(_background);
            }
        }

        public bool is_power
        {
            get
            {
                return GMData.inst.chaoting.power_party_background == _background;
            }
        }

        public bool is_first_select
        {
            get
            {
                return this == GMData.inst.chaoting.other_partys.first();
            }
        }

        [JsonProperty]
        internal string _background;

        [JsonProperty]
        public double prestige;
        
    }
}
