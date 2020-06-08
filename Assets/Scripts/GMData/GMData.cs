using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniRx.Async;
using UnityEngine;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class GMData
    {
        public static GMData inst;

        public int days
        {
            get
            {
                return _days;
            }
        }

        [JsonProperty]
        internal int _days;

        [JsonProperty]
        public double economy;

        [JsonProperty]
        public Taishou taishou;

        //internal Economy economy = new Economy();
        [JsonProperty]
        public List<Depart> departs = new List<Depart>();

        [JsonProperty]
        public List<Task> tasks = new List<Task>();

        [JsonProperty]
        public List<Pop> pops = new List<Pop>();

        [JsonProperty]
        public EXPECT_TAX_ROOT tax_expect = new EXPECT_TAX_ROOT();

        public GMDate date = new GMDate();

        [JsonProperty]
        internal List<string> record = new List<string>();

        internal bool quit;

        internal static void New(InitData initData)
        {
            inst = new GMData(initData);
        }

        //internal static IEnumerable<GEvent> GenerateEvent()
        //{
        //    foreach(var gevent in GEvent.Enumerate())
        //    {
        //        if(gevent is GEventDepart)
        //        {
        //            var departEvnet = gevent as GEventDepart;
        //            foreach (var depart in inst.listDepart)
        //            {
        //                departEvnet.setDepart(depart.def.pyObj);
        //                if (departEvnet.isTrigger())
        //                {
        //                    yield return gevent;
        //                }
        //            }
        //        }
        //        else if (gevent.isTrigger())
        //        {
        //            yield return gevent;
        //        }
        //    }

        //    foreach(var taskDef in TaskDef.Enumerate())
        //    {
        //        if (taskDef.isStart())
        //        {
        //            if (taskDef.start_event != null)
        //            {
        //                yield return GEvent.getEvent(taskDef.start_event);
        //            }

        //            GMData.inst.listTask.Add(new Task(taskDef));
        //        }
        //    }

        //    for (int i = 0; i < inst.listTask.Count(); i++)
        //    {
        //        var task = inst.listTask[i];
        //        if (task.isFinished())
        //        {
        //            inst.listTask.RemoveAt(i);

        //            yield return GEvent.getEvent(task.def.finish_event);
        //        }
        //    }
        //}

        internal async UniTask DaysInc(Func<EventDef.Interface, UniTask> act)
        {
            foreach (var gevent in EventDef.Generate())
            {
                await act(gevent);
            }

            foreach (var gevent in Task.DaysInc())
            {
                await act(gevent);
            }

            foreach (var gevent in Depart.DaysInc())
            {
                await act(gevent);
            }

            //Debug.Log("DaysInc_3");
            _days++;

            //Debug.Log(listTask[0].def.is_start());
        }

        internal Depart FindDepartByColor(string color)
        {
            return departs.SingleOrDefault(x => x.color == color);
        }

        //internal double collectTaxExpect(string level)
        //{
        //    var levelinfo = TaxLevelDef.getInfo(level);

        //    return EXPECT_TAX.Expect(levelinfo.rate);
        //}

        //internal void collectTaxStart(string level)
        //{
        //    var levelinfo = TaxLevelDef.getInfo(level);

        //    EXPECT_TAX.Start(levelinfo.rate);

        //    //foreach(var pop in GMData.inst.listPop.Where(x=>x.def.isTax))
        //    //{
        //    //    pop
        //    //}
        //}

        //internal double collectTaxFinish()
        //{
        //    EXPECT_TAX.Finish();
        //    return EXPECT_TAX.histroy.Last().taxed.value;
        //}

        internal GMData()
        {

        }

        private GMData(InitData InitData)
        {
            inst = this;

            _days = 1;

            taishou = new Taishou(InitData.taishou.name, InitData.taishou.age, InitData.taishou.background);

            //economy.value = 100;

            foreach (var elem in DepartDef.Enumerate())
            {
                var depart = new Depart(elem);
                departs.Add(depart);
            }

            foreach (var elem in  TaskDef.Enumerate())
            {
                tasks.Add(new Task(elem));
            }
        }

    }
}
