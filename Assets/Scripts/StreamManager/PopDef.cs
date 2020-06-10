using System;
using System.IO;
using System.Collections.Generic;
using XLua;
using System.Linq;

namespace TaisEngine
{

    public class PopDef : BaseDef<PopDef.Interface>
    {
        [CSharpCallLua]
        public interface Interface
        {
            string name { get; }
            int sort { get; }
            bool is_tax { get; }
            bool is_family { get; }

            double? consume { get; }
        }

        //public Dictionary<string, Interface> dict = new Dictionary<string, Interface>();

        public PopDef(string mod, LuaTable luaTable) : base(luaTable, mod, "POP_DEF")
        {
            //LuaTable luaTable = luaenv.Global.Get<LuaTable>("POP_DEF");
            //if (luaTable == null)
            //{
            //    return;
            //}

            //foreach (var key in luaTable.GetKeys<string>())
            //{
            //    var value = luaTable.Get<Interface>(key);
            //    if (value != null)
            //    {
            //        dict.Add(key, luaTable.Get<Interface>(key));
            //    }
            //}
        }

        //internal static IEnumerable<PopDef.Interface> Enumerate()
        //{
        //    foreach (var mod in Mod.listMod.Where(x => x.content != null))
        //    {
        //        foreach (var pop in mod.content.popDef.dict)
        //        {
        //            yield return pop.Value;
        //        }
        //    }
        //}

        //internal static Interface Find(string key)
        //{
        //    foreach (var mod in Mod.listMod.Where(x => x.content != null))
        //    {
        //        if(mod.content.popDef.dict.ContainsKey(key))
        //        {
        //            return mod.content.popDef.dict[key];
        //        }
        //    }

        //    throw new Exception("can not find PopDef:" + key);
        //}
    }
}
