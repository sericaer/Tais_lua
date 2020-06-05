using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniRx.Async;
using UnityEngine;

namespace TaisEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class GMData
    {
        internal static GMData inst;
        internal static void New(InitData initData)
        {
            inst = new GMData(initData);
        }

        internal int days
        {
            get
            {
                return _days;
            }
        }

        [JsonProperty]
        internal int _days;

        [JsonProperty]
        internal Taishou taishou;


        //internal Economy economy = new Economy();
        [JsonProperty]
        internal List<Depart> departs = new List<Depart>();

        [JsonProperty]
        internal List<Task> tasks = new List<Task>();

        [JsonProperty]
        internal List<Pop> pops = new List<Pop>();

        internal GMDate date = new GMDate();

        internal bool quit;

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
            Debug.Log("DaysInc_0");
            foreach (var gevent in EventDef.Generate())
            {
                await ProcessEvent(gevent, act);
            }

            foreach (var gevent in Task.DaysInc())
            {
                await ProcessEvent(gevent, act);
            }

            foreach (var gevent in Depart.DaysInc())
            {
                await ProcessEvent(gevent, act);
            }

            //Debug.Log("DaysInc_3");
            _days++;

            //Debug.Log(listTask[0].def.is_start());
        }

        internal async UniTask ProcessEvent(EventDef.Interface gevent, Func<EventDef.Interface, UniTask> act)
        {
            if(gevent.hide)
            {
                gevent.options["OPTION_1"].selected();
                return;
            }

            await act(gevent);
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
