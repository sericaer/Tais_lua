using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace TaisEngine
{
    public class Task
    {
        public readonly string key;

        public double curr_percent;
        public double curr_speed;

        internal static IEnumerable<EventDef.Interface> DaysInc()
        {
            for (int i = 0; i < GMData.inst.listTask.Count(); i++)
            {
                var task = GMData.inst.listTask[i];

                task.curr_percent += task.curr_speed;

                if (task.curr_percent.Equals(100))
                {
                    yield return EventDef.Find(task.def.finish_event());
                }
            }
        }


        internal Task(TaskDef.Interface def)
        {
            this.key = def.name;

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
                return TaskDef.Find(key);
            }
        }
    }
}
