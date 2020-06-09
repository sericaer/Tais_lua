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
            return list.Where(y => y.valid && y.def.tax_effect != null).Select(y => (y.name, y.def.tax_effect()));
        }

        public static IEnumerable<(string name, double value)> exist_crop_growing_effects(this List<Buffer> list)
        {
            return list.Where(y => y.valid && y.def.crop_growing_effect != null).Select(y => (y.name, y.def.crop_growing_effect()));
        }

        public static IEnumerable<(string name, double value)> exist_consume_effects(this List<Buffer> list)
        {
            return list.Where(y => y.valid && y.def.consume_effect != null).Select(y => (y.name, y.def.consume_effect()));
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

        public static void set_valid(this List<Buffer> list, string name)
        {
            var buffer = list.Single(x => x.name == name);
            if(buffer.def.group != "")
            {
                var groups = list.FindAll(x => x.def.group == buffer.def.group);
                foreach (var member in groups)
                {
                    member.valid = false;
                }
            }

            buffer.valid = true;
        }

        public static void set_invalid(this List<Buffer> list, string name)
        {
            var buffer = list.Single(x => x.name == name);
            buffer.valid = true;
        }

        public static bool is_valid(this List<Buffer> list, string name)
        {
            var buffer = list.Single(x => x.name == name);
            return buffer.valid;
        }
    }
}
