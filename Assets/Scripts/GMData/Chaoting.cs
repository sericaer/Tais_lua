﻿using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    [JsonObject(MemberSerialization.OptIn)]
    public class Chaoting
    {
        public Chaoting(string power_party, int reg_pop_num, double prestige)
        {
            power_party_background = power_party;
            _year_report_pop = reg_pop_num;
            pre_report_pop = reg_pop_num;

            this.prestige = prestige;
        }

        [JsonProperty]
        public int pre_report_pop;

        [JsonProperty]
        public double prestige;

        public Party power_party
        {
            get
            {
                return GMData.inst.parties.find(power_party_background);
            }
            set
            {
                power_party_background = value._background;
            }
        }

        public List<Party> other_partys
        {
            get
            {
                return GMData.inst.parties.Where(x => x.background.name != power_party_background).ToList();
            }
        }

        public int year_report_pop
        {
            get
            {
                return _year_report_pop;
            }
            set
            {
                pre_report_pop = _year_report_pop;
                _year_report_pop = value;
            }
        }

        public int year_report_tax
        {
            get
            {
                return (int)year_report_tax_list.Sum(x => x.report_tax_value);
            }
        }

        public int year_expect_tax
        {
            get
            {
                return (int)year_expect_tax_list.Sum(x => x.report_tax_value);
            }
        }

        public double report_tax
        {
            set
            {
                year_report_tax_list.Add((GMData.inst.days, value));
                year_expect_tax_list.Add((GMData.inst.days, GMData.inst.tax_report(year_report_pop)));
            }
        }

        [JsonProperty]
        internal List<(int days, double report_tax_value)> year_report_tax_list = new List<(int days, double report_tax_value)>();

        [JsonProperty]
        internal List<(int days, double report_tax_value)> year_expect_tax_list = new List<(int days, double report_tax_value)>();

        [JsonProperty]
        internal int _year_report_pop;

        [JsonProperty]
        internal string power_party_background;
    }
}
