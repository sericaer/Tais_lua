using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace TaisEngine
{
    public class BackgroundDef : BaseDef<BackgroundDef.Interface>
    {

        [CSharpCallLua]
        public interface Interface
        {
            string name { get; }

            Dictionary<string, double> relation { get; }
        }

        //public Dictionary<string, Interface> dict = new Dictionary<string, Interface>();

        public BackgroundDef(string mod, LuaTable luaTable) : base(luaTable, mod, "BACKGROUND_DEF")
        {
            //LuaTable luaTable = luaenv.Global.Get<LuaTable>("BACKGROUND_DEF");
            //if(luaTable == null)
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


        //internal static IEnumerable<BackgroundDef.Interface> Enumerate()
        //{
        //    foreach (var mod in Mod.listMod.Where(x => x.content != null))
        //    {
        //        foreach (var bk in mod.content.backgroundDef.dict)
        //        {
        //            yield return bk.Value;
        //        }
        //    }
        //}
    }
}
