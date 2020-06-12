using System;
using System.IO;
using System.Collections.Generic;
using XLua;
using System.Linq;

namespace TaisEngine
{
    public class DepartDef : BaseDef<DepartDef.Interface>
    {
        [CSharpCallLua]
        public interface Interface
        {
            string name { get; }
            List<double> color { get; }
            Dictionary<string, double> pop_init { get; }
        }

        //public Dictionary<string, Interface> dict = new Dictionary<string, Interface>();

        public DepartDef(string mod, LuaTable luaTable) : base(luaTable, mod, "DEPART")
        {

        }

        //internal static IEnumerable<DepartDef.Interface> Enumerate()
        //{
        //    foreach (var mod in Mod.listMod.Where(x => x.content != null))
        //    {
        //        foreach (var depart in mod.content.departDef.dict)
        //        {
        //            yield return depart.Value;
        //        }
        //    }
        //}

        //internal static Interface Find(string name)
        //{
        //    foreach (var mod in Mod.listMod.Where(x => x.content != null))
        //    {
        //        if (mod.content.popDef.dict.ContainsKey(key))
        //        {
        //            return mod.content.popDef.dict[key];
        //        }
        //    }

        //    throw new Exception("can not find PopDef:" + key);
        //}
    }

}
