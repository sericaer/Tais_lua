using System;
using System.Collections.Generic;
using System.Linq;
using XLua;

namespace TaisEngine
{
    public class BaseDef<T>
    {
        public static List<BaseDef<T>> all = new List<BaseDef<T>>();

        public string mod;
        public Dictionary<string, T> dict;


        public BaseDef(LuaTable luaTable, string mod, string name)
        {
            this.mod = mod;

            dict = new Dictionary<string, T>();

            LuaTable subTable = luaTable.Get<LuaTable>(name);
            if (subTable == null)
            {
                return;
            }

            foreach (var key in subTable.GetKeys<string>())
            {
                var value = subTable.Get<T>(key);
                if (value != null)
                {
                    dict.Add(key, subTable.Get<T>(key));
                }
            }

            all.Add(this);
        }

        internal static IEnumerable<T> Enumerate()
        {
            foreach (var elem in all)
            {
                foreach (var value in elem.dict.Values)
                {
                    yield return value;
                }
            }
        }

        internal static T Find(string name)
        {
            foreach (var elem in all)
            {
                if (elem.dict.ContainsKey(name))
                {
                    return elem.dict[name];
                }
            }

            throw new Exception("can not find:" + name);
        }
    }
}
