using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class testTable : MonoBehaviour
{
    public List<TTT> tableData = new List<TTT>();

    RecordData recordData;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, double> dictBaseData = new Dictionary<string, double>();
        dictBaseData.Add("AAAA|bbb|ccc", 100);
        dictBaseData.Add("AAAA|bbb|ddd", 100);
        dictBaseData.Add("AAAA|bba|cca", 100);

        Dictionary<string, double> dictbuffer = new Dictionary<string, double>();
        dictbuffer.Add("AAAA|bbb|ccc|BUFF_1", 0.2);
        dictbuffer.Add("AAAA|bbb|ddd|BUFF_2", -0.2);
        dictbuffer.Add("AAAA|bba|cca|BUFF_3", -0.3);
        dictbuffer.Add("AAAA|bbb|BUFF_4",0.4);

        recordData = new RecordData(dictBaseData, dictbuffer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

public class RecordData
{
    public List<(string name, double percent, double value)> showBuffers
    {
        get
        {
            return dictbuffer.Select(x => (x.Key, x.Value, x.Value * getBaseValue(BufferKey.GetDataKey(x.Key)))).ToList();
        }

    }

    public List<(string name, double baseValue, double realValue)> showData
    {
        get
        {
            return dictBaseData.Select(x => (x.Key,
                                             x.Value,
                                             x.Value * dictbuffer.Where(y => y.Key.Contains(x.Key))
                                                                 .Select(y => y.Value)
                                                                 .Aggregate(1, (double m, double n) => m * n))).ToList();
        }
    }


    public RecordData(Dictionary<string, double> dictBaseData, Dictionary<string, double> dictbuffer)
    {
        this.dictBaseData = dictBaseData;
        this.dictbuffer = dictbuffer;
    }

    class BufferKey
    {
        public static string GetDataKey(string raw)
        {
            return raw.Substring(0, raw.LastIndexOf("|"));
        }
    }

    Dictionary<string, double> dictBaseData;

    Dictionary<string, double> dictbuffer;

    double getBaseValue(string key)
    {
        double rslt = 0.0;
        foreach (var elem in dictBaseData)
        {
            if (elem.Key.Contains(key))
            {
                rslt += elem.Value;
            }
        }

        return rslt;
    }
}

public class BufferKey
{
    public static string GetDataKey(string raw)
    {
        return raw.Substring(0, raw.LastIndexOf("|"));
    }
}

public class TTT
{
    public string key;
    public int value;

    public TTT(string k, int v)
    {
        key = k;
        value = v;
    }
}