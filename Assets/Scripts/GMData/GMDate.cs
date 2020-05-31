using System;
using UnityEngine.UI.Extensions;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    public class GMDate
    {
        internal bool isSpring
        {
            get
            {
                return (month < 4);
            }
        }
        internal bool isSummer
        {
            get
            {
                return (month < 7 && month >=4);
            }
        }
        internal bool isAutumn
        {
            get
            {
                return (month < 10 && month >= 7);
            }
        }
        internal bool isWinter
        {
            get
            {
                return (month <= 12 && month >= 10);
            }
        }

        public int total_days
        {
            get
            {
                return GMData.inst.days;
            }
        }

        public int year
        {
            get
            {
                if(total_days == -1)
                {
                    return -1;
                }

                return total_days % 360 == 0 ? total_days / 360 : total_days / 360 + 1;
            }

        }

        public int month
        {
            get
            {
                if (total_days == -1)
                {
                    return -1;
                }

                return total_days % 30 == 0 ? (total_days % 360 == 0 ? 360 : total_days % 360) / 30 : total_days % 360 / 30 + 1;
            }
        }

        public int day
        {
            get
            {
                if (total_days == -1)
                {
                    return -1;
                }

                return total_days % 30 == 0 ? 30 : total_days % 30;
            }
        }

        public override string ToString()
        {
            if (total_days == 0)
            {
                return "--";
            }

            return Mod.GetLocalString("date", year, month, day);
        }
    }
}
