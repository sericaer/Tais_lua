using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XLua;
using Tools;

namespace TaisEngine
{
    public class EventDef
    {
        [CSharpCallLua]
        public interface Interface
        {
            bool hide { get; }
            Func<bool> trigger { get; }
            Func<int> occur_days { get; }
            List<object> title();
            List<object> desc();

            Dictionary<string, Option> options { get; }
        }

        [CSharpCallLua]
        public interface Option
        {
            List<object> desc();
            List<List<object>> tooltip();
            void selected();
            string next_event();
        }

        public class EventCommonDef : BaseDef<EventCommonDef.Interface>
        {
            [CSharpCallLua]
            public interface Interface : EventDef.Interface
            {

            }

            public EventCommonDef(LuaTable luaTable, string mod) : base(luaTable, mod, "COMMON")
            {
            }
        }

        public class EventDepartDef : BaseDef<EventDepartDef.Interface>
        {
            [CSharpCallLua]
            public interface Interface : EventDef.Interface
            {

            }

            public EventDepartDef(LuaTable luaTable, string mod) : base(luaTable, mod, "DEPART")
            {
            }
        }

        public class EventPopDef : BaseDef<EventPopDef.Interface>
        {
            [CSharpCallLua]
            public interface Interface : EventDef.Interface
            {

            }

            public EventPopDef(LuaTable luaTable, string mod) : base(luaTable, mod, "POP")
            {
            }
        }

        internal EventCommonDef globalEvent;
        internal EventDepartDef departEvent;
        internal EventPopDef popEvent;

        internal EventDef(string mod, LuaTable luaTable) 
        {
            globalEvent = new EventCommonDef(luaTable, mod);
            departEvent = new EventDepartDef(luaTable, mod);
            popEvent = new EventPopDef(luaTable, mod);

            Log.INFO($"load event count:{globalEvent.dict.Count() + departEvent.dict.Count() + popEvent.dict.Count()}");
        }

        internal static IEnumerable<EventDef.Interface> Generate()
        {
            Debug.Log("0");

            foreach (var eventDef in EventCommonDef.all)
            {
                foreach (var gevent in eventDef.dict.Values)
                {
                    if(isOccur(gevent))
                    {
                        yield return gevent;
                    }
                }
            }

            Debug.Log("1");
            foreach (var eventDef in EventDepartDef.all)
            {
                foreach (var gevent in eventDef.dict.Values)
                {
                    foreach (var depart in GMData.inst.departs)
                    {
                        ToLua.curr_depart = depart;

                        if (isOccur(gevent))
                        {
                            yield return gevent;
                        }

                        ToLua.curr_depart = null;
                    }
                }
            }


            Debug.Log("2");
            foreach (var eventDef in EventPopDef.all)
            {
                foreach (var gevent in eventDef.dict.Values)
                {
                    foreach (var pop in GMData.inst.pops)
                    {
                        ToLua.curr_pop = pop;

                        if (isOccur(gevent))
                        {
                            yield return gevent;
                        }

                        ToLua.curr_pop = null;
                    }

                }
            }
        }

        internal static Interface find(string next_event)
        {
            Interface gevent = EventCommonDef.FindOrDefault(next_event);
            if (gevent != null)
            {
                return gevent;
            }

            gevent = EventDepartDef.FindOrDefault(next_event);
            if (gevent != null)
            {
                return gevent;
            }

            throw new Exception("can not find " + next_event);
        }

        internal static bool isOccur(EventDef.Interface gevent)
        {
            if (gevent.trigger != null)
            {
                if (gevent.trigger())
                {
                    int days = 1;
                    if (gevent.occur_days != null)
                    {
                        days = gevent.occur_days();
                    }

                    return Tools.GRandom.isOccurDays(days);
                }
            }
            else
            {
                if (gevent.occur_days != null)
                {
                    return Tools.GRandom.isOccurDays(gevent.occur_days());
                }
            }

            return false;
        }
    }
}
