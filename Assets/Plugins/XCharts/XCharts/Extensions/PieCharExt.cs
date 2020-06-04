using UnityEngine;
using XCharts;
using System;
using System.Collections.Generic;

namespace XCharts
{
    public class PieCharExt : MonoBehaviour
    {
        public PieChart chart;

        public Func<IEnumerable<(string name, double value)>> funcGetData;

        // Use this for initialization
        void Start()
        {
            if (funcGetData != null)
            {
                var serie = chart.series.GetSerie("serie1");
                serie.ClearData();

                foreach (var elem in funcGetData())
                {
                    chart.AddData(0, (float)elem.value, elem.name);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
