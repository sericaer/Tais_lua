using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    public static class Extensions
    {
        public static Buffer find(this List<Buffer> list, string name)
        {
            return list.SingleOrDefault(x=>x.name == name);
        }

        public static Pop find(this List<Pop> list, string name)
        {
            return list.SingleOrDefault(x => x.name == name);
        }

        public static Depart find(this List<Depart> list, string name)
        {
            return list.SingleOrDefault(x => x.name == name);
        }

        public static Task find(this List<Task> list, string name)
        {
            return list.SingleOrDefault(x => x.name == name);
        }
    }
}
