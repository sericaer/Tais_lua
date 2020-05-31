using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace TaisEngine
{
    public class Task
    {
        public readonly string key;

        internal static IEnumerable<GEvent> DaysInc()
        {
            for (int i = 0; i < GMData.inst.listTask.Count(); i++)
            {
                var task = GMData.inst.listTask[i];

                task.def.curr_percent += task.def.curr_speed;

                if (task.def.curr_percent.Equals(100))
                {
                    task.def.finish();
                    yield return Mod.getEvent(task.def.finish_event());
                }
            }

        }


        internal Task(TaskDef def)
        {
            this.def = def;

            this.def.curr_percent = 0;
            this.def.curr_speed = def.base_speed;
        }

        internal TaskDef def;
    }
}
