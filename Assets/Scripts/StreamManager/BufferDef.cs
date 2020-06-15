using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tools;
using UnityEngine;
using XLua;

namespace TaisEngine
{

    public class BufferDef
    {
        public BufferDepartDef bufferDepart;
        public BufferPopDef bufferPop;

        [CSharpCallLua]
        public interface Interface
        {
            string name { get; }
            string group { get; }
            int duration { get; }
            bool multiple { get; }

            Func<double> tax_effect { get; }
            Func<double> crop_growing_effect { get; }
            Func<double> consume_effect { get; }
        }

        internal static Interface Find(string name)
        {
            Interface def = BufferDepartDef.FindOrDefault(name);
            if (def != null)
            {
                return def;
            }

            def = BufferPopDef.FindOrDefault(name);
            if(def != null)
            {
                return def;
            }

            throw new Exception("can not find" + name);
        }

        public class BufferDepartDef : BaseDef<BufferDepartDef.Interface>
        {
            [CSharpCallLua]
            public interface Interface : BufferDef.Interface
            {
            }

            public BufferDepartDef(string mod, LuaTable luaTable) : base(luaTable, mod, "DEPART")
            {
            }
        }

        public class BufferPopDef : BaseDef<BufferPopDef.Interface>
        {
            [CSharpCallLua]
            public interface Interface : BufferDef.Interface
            {
            }

            public BufferPopDef(string mod, LuaTable luaTable) : base(luaTable, mod, "POP")
            {
            }
        }

        public BufferDef(string mod, LuaTable luaTable)
        {
            bufferDepart = new BufferDepartDef(mod, luaTable);
            bufferPop = new BufferPopDef(mod, luaTable);

            Log.INFO($"load buffer count:{bufferDepart.dict.Count() + bufferPop.dict.Count()}");
        }

        //public double init_speed
        //{
        //    get
        //    {
        //        return Mod.engine.Operations.GetMember<double>(pyObj, "init_speed");
        //    }
        //}

        //public string start_event
        //{
        //    get
        //    {
        //        return Mod.engine.Operations.GetMember<string>(pyObj, "start_event");
        //    }
        //}

        //public string finish_event
        //{
        //    get
        //    {
        //        return Mod.engine.Operations.GetMember<string>(pyObj, "finish_event");
        //    }
        //}

        //public double curr_speed
        //{
        //    get
        //    {
        //        return Mod.engine.Operations.GetMember(pyObj, "curr_speed");
        //    }
        //    set
        //    {
        //        Mod.engine.Operations.SetMember(pyObj, "curr_speed", value);
        //    }
        //}

        //public double curr_percent
        //{
        //    get
        //    {
        //        return Mod.engine.Operations.GetMember(pyObj, "curr_percent");
        //    }
        //    set
        //    {
        //        Mod.engine.Operations.SetMember(pyObj, "curr_percent", value>100 ? 100 : value);
        //    }
        //}

        //public TaskDef(string name, object pyObj, Mod mod) : base(name, pyObj, mod)
        //{
        //    mod.dictTask.Add(name, this);

        //    Mod.engine.Operations.TryGetMember<Func<bool>>(pyObj, "is_start", out _isStart);
        //}

        //public bool isStart()
        //{
        //    return _isStart();
        //}

        //internal void setData(string key, object value)
        //{
        //    scope.SetVariable(key, value);
        //}

        //private Func<bool> _isStart;

        //private ScriptScope scope;
    }
}
