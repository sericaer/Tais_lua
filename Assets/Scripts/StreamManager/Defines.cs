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
        Dictionary<string, TAX_LEVEL_INTFERFACE> tax_level_define;

        [CSharpCallLua]
        public interface TAX_LEVEL_INTFERFACE
        {
            double income { get; }
            double consume { get; }
        }

        public Defines(string mod, LuaTable luaTable)
        {
            tax_level_define = null;

            if (luaTable.ContainsKey("TAX_LEVEL"))
            {
                tax_level_define = new Dictionary<string, TAX_LEVEL_INTFERFACE>();

                var taxLevelTable = luaTable.Get<LuaTable>("TAX_LEVEL");
                foreach (var key in taxLevelTable.GetKeys<string>())
                {
                    tax_level_define.Add(key, taxLevelTable.Get<TAX_LEVEL_INTFERFACE>(key));
                }
            }
        }

        internal static double getExpectTax(Economy.TAX_LEVEL level)
        {
            if(level == Economy.TAX_LEVEL.level0)
            {
                return 0.0;
            }

            foreach (var mod in Mod.listMod)
            {
                if(mod.content != null && mod.content.defines.tax_level_define != null)
                {
                    return mod.content.defines.tax_level_define[level.ToString()].income;
                }
            }

            throw new Exception();
        }

        internal static double getExpectTax(float f)
        {
            if (f.Equals(0.0))
            {
                return 0.0;
            }

            int start = (int)f;
            int offset = (int)((f - (float)start) * 10);

            double start_income = 0.0;
            double next_income = 0.0;
            foreach (var mod in Mod.listMod)
            {
                if (mod.content != null && mod.content.defines.tax_level_define != null)
                {
                    start_income = mod.content.defines.tax_level_define[$"level{start}"].income;
                    next_income = mod.content.defines.tax_level_define[$"level{start+1}"].income;

                    return start_income + (next_income - start_income) * offset / 10;
                }
            }

            throw new Exception();
        }
    }
}
