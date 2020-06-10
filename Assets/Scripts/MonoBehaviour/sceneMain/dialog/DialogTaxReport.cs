using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XCharts;
using TaisEngine;

public class DialogTaxReport : Dialog
{
    //public List<BUFFER_INFO> listBufferTitle;
    public List<BUFFER_INFO> listBufferInfo = new List<BUFFER_INFO>();

    public List<(string name, double value)> listTaxInfo;

    public PieCharExt pieChartExt;

    public void onClickConfrim()
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        listBufferInfo.Clear();

        var buffInfo = GMData.inst.tax_current.GetBufferInfo();
        listBufferInfo.AddRange(buffInfo.Select(x => new BUFFER_INFO() { name = TaisEngine.Mod.GetLocalString(x.name), 
                                                                         value = x.value.ToString("N1")}));

        if(listBufferInfo.Count() == 0)
        {
            listBufferInfo.Add(new BUFFER_INFO() { name = "--", value = "--" });
        }
        listTaxInfo = GMData.inst.tax_current.GetTaxInfo();

        pieChartExt.funcGetData = () =>
        {
            return listTaxInfo.Select(x => (TaisEngine.Mod.GetLocalString(x.name), x.value));
        };

    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class BUFFER_INFO
{
    public string name;
    public string value;
}