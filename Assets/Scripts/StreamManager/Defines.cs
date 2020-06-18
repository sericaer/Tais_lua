using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace TaisEngine
{
    public class Defines
    {
        Dictionary<string, double> tax_level_define;

        public Defines(string mod, LuaTable luaTable)
        {
            tax_level_define = luaTable.ContainsKey("tax_level") ? luaTable.Get<Dictionary<string, double>>("tax_level") : null;
        }

        internal static double getExpectTax(TAX_LEVEL level)
        {
            if(level == TAX_LEVEL.level0)
            {
                return 0.0;
            }

            foreach (var mod in Mod.listMod)
            {
                if(mod.content != null && mod.content.defines.tax_level_define != null)
                {
                    return mod.content.defines.tax_level_define[level.ToString()];
                }
            }

            throw new Exception();
        }
    }
}
