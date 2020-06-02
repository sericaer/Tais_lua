using System;
using Newtonsoft.Json;
using UnityEngine;

namespace TaisEngine
{
    public class GMSerialize
    {
        internal static string savePath = Application.streamingAssetsPath + "/save/";

        internal static void Save(GMData data)
        {
            string serialData = JsonConvert.SerializeObject(data);
        }
    }
}
