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
        internal List<Depart> listDepart = new List<Depart>();

        [JsonProperty]
        internal List<Task> listTask = new List<Task>();

        [JsonProperty]
        internal List<Pop> listPop = new List<Pop>();

        internal GMDate date = new GMDate();

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

        internal async UniTask DaysInc(Func<TaisEngine.GEvent, UniTask> act)
        {
            Debug.Log("DaysInc_0");
            foreach (var gevent in Mod.GenerateEvent())
            {
                Debug.Log("DaysInc_0_0");
                await act(gevent);

                Debug.Log("DaysInc_0_1");
            }

            Debug.Log("DaysInc_1");
            foreach (var gevent in Task.DaysInc())
            {
                await act(gevent);
            }

            //Debug.Log("DaysInc_2");
            //foreach (var gevent in Depart.DaysInc())
            //{
            //    await act(gevent);
            //}

            //Debug.Log("DaysInc_3");
            _days++;

            Debug.Log(listTask[0].def.is_start());
        }

        internal Depart FindDepartByColor(string color)
        {
            return listDepart.SingleOrDefault(x => x.color == color);
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

        private GMData(InitData InitData)
        {
            inst = this;

            _days = 1;

            taishou = new Taishou(InitData.taishou.name, InitData.taishou.age, InitData.taishou.background);

            //economy.value = 100;

            foreach (var elem in Mod.EnumerateDepart())
            {
                var depart = new Depart(elem);
                listDepart.Add(depart);
            }

            foreach (var elem in  Mod.EnumerateTask())
            {
                listTask.Add(new Task(elem));
            }
        }

    }
}
