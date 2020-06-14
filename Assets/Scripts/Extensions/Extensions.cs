using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace TaisEngine
{
    public static class Extensions
    {
        public static IEnumerable<(string name, double value)> exist_tax_effects(this List<Buffer> list)
        {
            return list.Where(y => y.def.tax_effect != null).Select(y => (y.name, y.def.tax_effect()));
        }

        public static IEnumerable<(string name, double value)> exist_crop_growing_effects(this List<Buffer> list)
        {
            return list.Where(y => y.def.crop_growing_effect != null).Select(y => (y.name, y.def.crop_growing_effect()));
        }

        public static IEnumerable<(string name, double value)> exist_consume_effects(this List<Buffer> list)
        {
            return list.Where(y => y.def.consume_effect != null).Select(y => (y.name, y.def.consume_effect()));
        }
    }

    [LuaCallCSharp]
    public static class Extensions_ToLua
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

        public static Party find(this List<Party> list, string name)
        {
            return list.SingleOrDefault(x => x._background == name);
        }

        public static void set_valid(this List<Buffer> list, string name)
        {
            var buffer = list.SingleOrDefault(x => x.name == name);
            if(buffer != null && !buffer.def.multiple)
            {
                return;
            }

            var def = BufferDef.Find(name);
            list.RemoveAll(x => x.def.group == def.group);

            var new_buff = new Buffer(def);
            list.Add(new_buff);
        }

        public static void set_invalid(this List<Buffer> list, string name)
        {
            list.RemoveAll(x => x.name == name);
        }

        public static bool is_valid(this List<Buffer> list, string name)
        {
            var buffer = list.Find(x => x.name == name);
            return buffer != null;
        }

        public static int exist_days(this List<Buffer> list, string name)
        {
            var buffer = list.Find(x => x.name == name);
            return buffer.exist_days;
        }
    }
}
