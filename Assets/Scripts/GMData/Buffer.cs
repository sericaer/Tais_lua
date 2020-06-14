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
    [JsonObject(MemberSerialization.OptIn)]
    public class Buffer
    {
        [JsonProperty]
        public string name;

        public BufferDef.Interface def
        {
            get
            {
                return BufferDef.Find(name);
            }
        }

        public int exist_days
        {
            get
            {
                return GMData.inst.days - start_days;
            }
        }

        [JsonProperty]
        internal int start_days;


        internal int end_days_expect
        {
            get
            {
                if(def.duration <= 0)
                {
                    return 0;
                }

                return start_days + def.duration;
            }
        }

        internal Buffer(BufferDef.Interface def)
        {
            this.name = def.name;
            this.start_days = GMData.inst.days;
        }

        internal Buffer()
        {

        }
    }
}
