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

        public bool valid { get; set; }

        public BufferDef.Interface def
        {
            get
            {
                return BufferDef.Find(name);
            }
        }

        
        internal Buffer(BufferDef.Interface def)
        {
            this.name = def.name;
        }

        internal Buffer()
        {

        }
    }
}
