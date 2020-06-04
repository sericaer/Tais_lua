using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using UnityEngine;

namespace TaisEngine
{
    public class Config
    {
        public static Config inst;
        public static string configPath = $"{Application.streamingAssetsPath}/config.json";

        public static void Load()
        {
            inst = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
        }

        public static void Save()
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(inst));
        }

        public static void Reset()
        {
            inst.select_mods.Clear();
            inst.select_mods.Add("native");
        }

        public string lang;


        public List<string> select_mods;
    }
}
