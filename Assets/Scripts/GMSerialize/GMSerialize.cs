using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

using Tools;

namespace TaisEngine
{
    public class GMSerialize
    {
        internal static string savePath = Application.streamingAssetsPath + "/save/";

        internal static void Save(string saveFileName, GMData data)
        {
            string fullPath = $"{savePath}{saveFileName}.save";

            Log.INFO($"save game path {fullPath}");

            if (File.Exists(fullPath))
            {
                throw new Exception("FILE_ALREADY_EXIT");
            }

            string serialData = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(fullPath, serialData);
        }

        internal static GMData Load(string saveFileName)
        {
            string fullPath = $"{savePath}{saveFileName}.save";

            Log.INFO($"load game path {fullPath}");

            string jsonStr = File.ReadAllText(fullPath);

            return JsonConvert.DeserializeObject<GMData>(jsonStr);
        }
    }
}
