using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace TaisEngine
{
    public class GMSerialize
    {
        internal static string savePath = Application.streamingAssetsPath + "/save/";

        internal static void Save(string saveFileName, GMData data)
        {
            string fullPath = $"{savePath}{saveFileName}.save";
            if (File.Exists(fullPath))
            {
                throw new Exception("FILE_ALREADY_EXIT");
            }

            string serialData = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(fullPath, serialData);

            Debug.Log(serialData);
        }
    }
}
