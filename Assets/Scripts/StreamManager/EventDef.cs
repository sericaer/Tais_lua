using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XLua;

namespace TaisEngine
{
    public class EventDef
    {
        [CSharpCallLua]
        public interface Interface
        {
            bool hide { get; }
            Func<double> occur_rate { get; }
            string title();
            string desc();

            Dictionary<string, Option> options { get; }
        }

        [CSharpCallLua]
        public interface Option
        {
            string desc();
            void selected();
            string next_event();
        }

        public class EventGlobalDef : BaseDef<EventGlobalDef.Interface>
        {
            [CSharpCallLua]
            public interface Interface : EventDef.Interface
            {

            }

            public EventGlobalDef(LuaTable luaTable, string mod) : base(luaTable, mod, "GLOBAL")
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

        internal EventGlobalDef globalEvent;
        internal EventDepartDef departEvent;

        internal EventDef(string mod, LuaTable luaTable) 
        {
            globalEvent = new EventGlobalDef(luaTable, mod);
            departEvent = new EventDepartDef(luaTable, mod);
        }

        internal static IEnumerable<EventDef.Interface> Generate()
        {
            foreach (var eventDef in EventGlobalDef.all)
            {
                foreach (var gevent in eventDef.dict.Values.Where(x=>x.occur_rate != null))
                {
                    if (Tools.GRandom.isOccur(gevent.occur_rate() * 100))
                    {
                        yield return gevent;
                    }
                }
            }

            foreach (var eventDef in EventDepartDef.all)
            {
                foreach (var gevent in eventDef.dict.Values.Where(x => x.occur_rate != null))
                {
                    foreach (var depart in GMData.inst.departs)
                    {
                        ToLua.curr_depart = depart;

                        if (Tools.GRandom.isOccur(gevent.occur_rate() * 100))
                        {
                            yield return gevent;
                        }
                    }

                }
            }

            ToLua.curr_depart = null;
        }

        internal static Interface find(string next_event)
        {
            Interface gevent = EventGlobalDef.FindOrDefault(next_event);
            if (gevent != null)
            {
                return gevent;
            }

            gevent = EventDepartDef.FindOrDefault(next_event);
            if (gevent != null)
            {
                return gevent;
            }

            return null;
        }
    }
}
