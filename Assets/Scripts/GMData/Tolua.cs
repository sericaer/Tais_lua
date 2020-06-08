using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    public class ToLua
    {
        public static GMData inst = GMData.inst;
        public static Depart curr_depart;

        //    public GMDate date
        //    {
        //        get
        //        {
        //            return GMData.inst.date;
        //        }
        //    }

        //    public Tasks tasks = new Tasks();

        //    public double collect_tax_expect(string level)
        //    {
        //        //var levelinfo = TaxLevelDef.getInfo(level);

        //        return EXPECT_TAX.Expect(0.001);
        //    }

        //    public void collect_tax_start(string level)
        //    {
        //        //var levelinfo = TaxLevelDef.getInfo(level);

        //        EXPECT_TAX.Start(0.001);
        //    }
        //}

        //[LuaCallCSharp]
        //public class Tasks
        //{
        //    public Task find(string name)
        //    {
        //        return GMData.inst.tasks.SingleOrDefault(x => x.name == name);
        //    }
        //}
    }
}
