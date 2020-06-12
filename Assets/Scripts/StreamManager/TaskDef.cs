using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;
using XLua;

namespace TaisEngine
{

    public class TaskDef : BaseDef<TaskDef.Interface>
    {
        public TaskDef(string mod, LuaTable luaTable) : base(luaTable, mod, "TASK")
        {
        }

        [CSharpCallLua]
        public interface Interface
        {
            string name { get; }

            double base_speed { get; }
            string finish_event();
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
