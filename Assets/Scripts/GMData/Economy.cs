using System;
using System.Linq;

using Newtonsoft.Json;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class Economy
    {
        [JsonProperty]
        public double value;

        [JsonProperty]
        internal double currTax_average;

        [JsonProperty]
        internal int validTaxChangedDays;

        internal int taxChangedDaysSpan = 60;

        internal enum TAX_LEVEL
        {
            level0,
            level1,
            level2,
            level3,
            level4,
            level5,
        }

        internal double currTax
        {
            get
            {
                return currTax_average * taxed_pop_num;
            }
            set
            {
                validTaxChangedDays = GMData.inst.days + taxChangedDaysSpan;
                currTax_average = value / taxed_pop_num;
            }
        }

        internal double maxTax
        {
            get
            {
                return taxed_pop_num * Defines.getExpectTax(TAX_LEVEL.level5);
            }
        }

        internal int taxed_pop_num
        {
            get
            {
                return GMData.inst.pops.Where(x => x.def.is_tax && !x.depart.cancel_tax).Sum(x => (int)x.num);
            }
        }

        internal double surplus
        {
            get
            {
                return currTax - GMData.inst.chaoting.expect_tax;
            }
        }

        internal void DayInc()
        {
            if(GMData.inst.date.day == 30)
            {
                value += surplus;
            }
        }

        internal bool local_tax_change_valid
        {
            get
            {
                return GMData.inst.days >= validTaxChangedDays;
            }
        }
    }
}
