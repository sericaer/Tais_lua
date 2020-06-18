using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniRx.Async;
using UnityEngine;
using XLua;
using Tools;

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
        public Taishou taishou;

        [JsonProperty]
        public Chaoting chaoting;

        [JsonProperty]
        public Economy economy = new Economy();

        [JsonProperty]
        public List<Depart> departs = new List<Depart>();

        [JsonProperty]
        public List<Task> tasks = new List<Task>();

        [JsonProperty]
        public List<Pop> pops = new List<Pop>();

        [JsonProperty]
        public List<Family> families = new List<Family>();

        [JsonProperty]
        public List<Party> parties = new List<Party>();

        //[JsonProperty]
        //public EXPECT_TAX_ROOT tax_expect = new EXPECT_TAX_ROOT();

        [JsonProperty]
        internal TAX_INFO tax_current;

        [JsonProperty]
        internal List<(int days, TAX_INFO tax_info)> tax_histroy = new List<(int days, TAX_INFO tax_info)>();

        [JsonProperty]
        internal double tax_rate;

        public GMDate date = new GMDate();

        [JsonProperty]
        internal List<string> record = new List<string>();

        internal List<List<Buffer>> allBuffers = new List<List<Buffer>>();

        public bool end_flag;


        internal static void New(InitData initData)
        {
            inst = new GMData(initData);
        }

        public double tax_collect_expect(double rate = double.Epsilon)
        {
            if(rate.Equals(double.Epsilon))
            {
                return tax_current.value;
            }

            if (rate.Equals(tax_rate) && tax_current != null)
            {
                return tax_current.value;
            }

            return new TAX_INFO(rate).value;
        }

        public void tax_collect_start(double rate)
        {
            tax_rate = rate;
            tax_current = new TAX_INFO(rate);
        }

        public double tax_collect_finish()
        {
            var rslt = tax_current.value;
            tax_histroy.Add((days, tax_current));

            tax_current = null;
            return rslt;
        }

        public double tax_report(int pop_num)
        {
            return pop_num * 0.0007;
        }

        internal async UniTask DaysInc(Func<EventDef.Interface, UniTask> act)
        {
            if(date.day == 1)
            {
                RecordHistroy();

                if(date.month == 1)
                {
                    chaoting.year_expect_tax_list.Clear();
                    chaoting.year_report_tax_list.Clear();
                }
            }


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

            foreach(var buffers in allBuffers)
            {
                buffers.RemoveAll(x => x.end_days_expect == GMData.inst.days);
            }

            if (tax_current != null)
            {
                tax_current.Update(tax_rate);
            }

            economy.DayInc();

            //Debug.Log("DaysInc_3");
            _days++;

            //Debug.Log(listTask[0].def.is_start());
        }

        private void RecordHistroy()
        {
            foreach(var pop in pops)
            {
                pop.histroy_rec.Add((days, new Pop.HISTROY_RECORD() { num = (int)pop.num }));
            }
        }

        internal Depart FindDepartByColor(string color)
        {
            return departs.SingleOrDefault(x => x.color == color);
        }


        internal GMData()
        {

        }

        private GMData(InitData InitData)
        {
            Log.INFO("new game start");

            inst = this;

            _days = 1;

            taishou = new Taishou(InitData.taishou.name, InitData.taishou.age, InitData.taishou.background);

            foreach(var background in BackgroundDef.Enumerate())
            {
                parties.Add(new Party(background.name));
            }
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

            chaoting = new Chaoting(BackgroundDef.Enumerate().OrderBy(x => Guid.NewGuid()).First().name, 
                pops.Where(x=>x.def.is_tax).Sum(y=>(int)y.num),
                100);

            economy.curr_tax_level = (float)chaoting.tax_level * 1.1f ;
        }
    }
}
