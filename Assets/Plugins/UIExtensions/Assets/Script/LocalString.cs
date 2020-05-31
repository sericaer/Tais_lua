using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityEngine.UI.Extensions
{
    [System.Serializable]
    public class LocalString
    {
        public enum COLOR
        {
            red,
            green,
            blue
        }

        internal static string getColorStr(COLOR color)
        {
            switch (color)
            {
                case COLOR.blue:
                    return "#4000FF";
                case COLOR.red:
                    return "#FF4000";
                case COLOR.green:
                    return "#00FF40";
                default:
                    throw new Exception();
            }
        }

        internal static string getTooltipDetail(string cause, double value, string format= null)
        {
            return $"{new LocalString.COLOR_DATA($"{Math.Round(value, 2)}", value > 0 ? LocalString.COLOR.green : LocalString.COLOR.red).ToString()}　　{new LocalString(cause).ToString()}";
        }

        public class COLOR_DATA
        {
            internal COLOR color;
            internal object data;

            public COLOR_DATA(object data, COLOR color)
            {
                this.data = data;
                this.color = color;
            }

            public override string ToString()
            {
                return $"<color={color}>{data}</color>";
            }
        }

        public static void LoadLangDir(string modname, string path)
        {
            var dict = new Dictionary<string, string>();
            foreach(var file in Directory.EnumerateFiles(path, "*.txt", SearchOption.TopDirectoryOnly))
            {
                if(!Path.GetFileName(file).StartsWith("text_"))
                {
                    continue;
                }

                foreach(var line in File.ReadLines(file))
                {
                    var splits = line.Split(':');
                    if(splits.Count() != 2)
                    {
                        throw new ArgumentException($"format error!, line:{line} in file:{file}");
                    }

                    if(dict.ContainsKey(splits[0]))
                    {
                        throw new ArgumentException($"already have the local string key, line:{line} in file:{file}");
                    }

                    dict.Add(splits[0], splits[1]);
                }
            }

            var lang = new DirectoryInfo(path).Name;
            if(!_dictLocal.ContainsKey(lang))
            {
                _dictLocal[lang] = new Dictionary<string, Dictionary<string, string>>();
            }

            _dictLocal[lang].Add(modname, dict);
        }

        public static string lang
        {
            get
            {
                return _lang;
            }
            set
            {
                if(!_dictLocal.ContainsKey(value))
                {
                    throw new ArgumentException("can not support lang:" + lang);
                }

                _lang = value;
            }
        }

        public static string[] langs
        {
            get
            {
                return _dictLocal.Keys.ToArray();
            }
        }

        public string format;
        public object[] ps;

        public LocalString(string format)
        {
            this.format = format;
        }

        public LocalString(string format, params object[] param) : this(format)
        {
            SetParam(param);
        }

        public void SetParam(params object[] param)
        {
            ps = param;
        }

        public override string ToString()
        {
#if UNITY_EDITOR
            if(!UnityEditor.EditorApplication.isPlaying)
            {
                return format;
            }
#endif

            var modName = _defaultMod;
            var formatId = format;

            if(formatId.All(x=>(x>='0' && x<='9')|| x == '.' || x== '+' || x=='-'))
            {
                return formatId;
            }

            if (format.Contains('.'))
            {
                var splits = format.Split('.');

                if (splits.Count() != 2)
                {
                    throw new ArgumentException($"error localstring format! {format}");
                }

                modName = splits[0];
                formatId = splits[1];
            }

            var realForamt = getReal(formatId, modName);
            var realParams = getRealParams(modName);
            if(realParams == null)
            {
                return realForamt;
            }
            return string.Format(realForamt, realParams);
        }

        public string[] getRealParams(string modName)
        {
            return ps == null ? null : ps.Select(x => getReal(x, modName)).ToArray();

        }

        private string getReal(object param, string modName)
        {
#if UNITY_STANDALONE_OSX
            return param.ToString();
#endif
            if (param.GetType() == typeof(COLOR_DATA))
            {
                var rawParam = param as COLOR_DATA;

                var realParam = getReal(rawParam.data, modName);

                var color = getColorStr(rawParam.color);
                return $"<color={color}>{realParam}</color>";
            }

            var strParam = param.ToString();

            if (param is string)
            {
                var rawParam = param as string;
                if (rawParam.Contains('.'))
                {
                    var splits = rawParam.Split('.');

                    if (splits.Count() != 2)
                    {
                        throw new ArgumentException($"error localstring format! {rawParam}");
                    }

                    modName = splits[0];
                    strParam = splits[1];
                }
            }

            if (!_dictLocal[_lang][modName].ContainsKey(strParam))
            {
                modName = _defaultMod;
            }

            if (_dictLocal[_lang][modName].ContainsKey(strParam))
            {
                return _dictLocal[_lang][modName][strParam]; 
            }

            return strParam;
        }

        private static string _defaultMod = "native";
        private static string _lang;
        private static Dictionary<string, Dictionary<string, Dictionary<string, string>>> _dictLocal = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
    }
}
