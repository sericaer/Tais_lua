using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class Task
    {
        [JsonProperty]
        public readonly string name;

        [JsonProperty]
        public double curr_percent;

        [JsonProperty]
        public double curr_speed;

        [JsonProperty]
        public bool start
        {
            get
            {
                return _isStart;
            }
            set
            {
                curr_percent = 0;
                _isStart = value;
            }
        }

        //public TaskToMod toMod
        //{
        //    get
        //    {
        //        return new TaskToMod() { _funcName =()=> key,}
        //    }
        //}

        internal static IEnumerable<EventDef.Interface> DaysInc()
        {
            foreach(var task in GMData.inst.tasks.Where(x=>x.start))
            {
                task.curr_percent += task.curr_speed;

                if (task.curr_percent.Equals(100))
                {
                    var eventName = task.def.finish_event();
                    if(eventName != "")
                    {
                        yield return EventDef.EventGlobalDef.Find(eventName);
                    }
                    
                    task._isStart = false;
                }
            }
        }



        internal Task(TaskDef.Interface def)
        {
            this.name = def.name;

            this.curr_percent = 0;
            this.curr_speed = def.base_speed;
        }

        internal Task()
        {

        }

        internal TaskDef.Interface def
        {
            get
            {
                return TaskDef.Find(name);
            }
        }

        private bool _isStart;
    }

}
