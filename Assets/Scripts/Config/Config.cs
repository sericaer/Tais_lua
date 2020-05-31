using System;
using System.IO;

using Newtonsoft.Json;

namespace TaisEngine
{
    public class Config
    {
        public static Config inst;

        public static void Load(string path)
        {
            inst = JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
        }

        public string lang;
    }
}
