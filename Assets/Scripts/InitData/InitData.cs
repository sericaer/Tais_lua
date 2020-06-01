using System;
using System.IO;

using Newtonsoft.Json;

using Tools;
using XLua;

namespace TaisEngine
{
    [LuaCallCSharp]
    public class InitData
    {
        public static InitData inst;

        public InitDataTaishou taishou;

        internal static InitData Random()
        {
            inst = new InitData()
            {
                taishou = InitDataTaishou.Random()
            };

            return inst;
        }

        internal static void Generate()
        {
            inst = new InitData()
            {
                taishou = new InitDataTaishou()
            };
        }
    }

    [LuaCallCSharp]
    public class InitDataTaishou
    {
        public static (int min, int max) ageRange = (25, 55);

        public int age;

        public string name;

        public string background;

        internal static InitDataTaishou Random()
        {
            return new InitDataTaishou()
            {
                name = PersonName.RandomFull,
                background = "SHIZU",
                age = GRandom.getNum(ageRange.min, ageRange.max)
            };
        }
    }
}
