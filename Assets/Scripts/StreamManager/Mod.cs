using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using XLua;

namespace TaisEngine
{
    partial class Mod
    {

        internal static string modRootPath = Application.streamingAssetsPath + "/mod/";
        internal static List<Mod> listMod = new List<Mod>();

        internal static void Load()
        {

            listMod.Clear();

            foreach (var path in Directory.EnumerateDirectories(modRootPath))
            {
                try
                {
                    var infoFile = $"{path}/info.json";
                    if (!File.Exists(infoFile))
                    {
                        continue;
                    }

                    listMod.Add(new Mod(path));
                }
                catch (Exception e)
                {
                    throw new Exception($"load mod {path} failed!", e);
                }

            }
        }

        internal static string GetLocalString(string arg)
        {
            object[] objs = { };
            return GetLocalString(arg, objs);
        }

        internal static IEnumerable<InitSelectDef> EnumerateInitSelect()
        {
            foreach (var mod in Mod.listMod.Where(x=>x.content != null))
            {
                foreach (var select in mod.content.dictInitSelect.Values)
                {
                    yield return select;
                }
            }
        }

        internal static string GetLocalString(string arg, params object[] objs)
        {
            try
            {
#if UNITY_EDITOR_OSX
            return arg;
#endif
                var args = arg.Split('|');

                string rslt = String.Join("", args.Select(x =>
                {

                    string tRslt = x;

                    foreach (var mod in listMod.Where(y => y.content != null))
                    {
                        if (!mod.content.dictlang.ContainsKey(Config.inst.lang))
                        {
                            continue;
                        }

                        if (mod.content.dictlang[Config.inst.lang].ContainsKey(x))
                        {
                            tRslt = mod.content.dictlang[Config.inst.lang][x];
                            if (objs.Count() != 0)
                            {
                                var test = objs.Select(y => y is string ? GetLocalString(y as string) : y).ToArray();
                                tRslt = string.Format(tRslt, test);
                            }
                            break;
                        }
                    }

                    return tRslt;
                }));

                return rslt;
            }
            catch(Exception e)
            {
                throw new Exception(arg, e);
            }

        }

        internal string path;
        internal Info info;
        internal Content content;

        //internal TaxLevelDef taxlevelDef;

        internal Mod(string modPath)
        {
            path = Path.GetFullPath(modPath);

            Debug.Log($"****************Read mod info {path} *************");

            info = JsonConvert.DeserializeObject<Info>(File.ReadAllText($"{path}/info.json"));

            if(!info.master && !Config.inst.select_mods.Contains(info.name))
            {
                return;
            }

            content = new Content();

            if (Directory.Exists($"{path}/lang/"))
            {
                loadLocalText(Directory.EnumerateDirectories($"{path}/lang/"));
            }

            var luaenv = DoLuas();

            content.Load(info.name, luaenv);

            LoadInitSelectDef(luaenv);
            //LoadPop(luaenv);
            //LoadBackground(luaenv);
            //LoadDepart(luaenv);
            //LoadEvent(luaenv);
            //LoadTask(luaenv);
        }

        private LuaEnv DoLuas()
        {
            var startRequire = @"require 'xlua.class_func'
require 'xlua.inner_def'";

            var luaDirs = new List<string>() {
                                                "INIT_SELECT",
                                                "POP",
                                                "BACKGROUND",
                                                "DEPART",
                                                "EVENT/COMMON",
                                                "EVENT/DEPART",
                                                "EVENT/POP",
                                                "TASK",
                                                "BUFFER/DEPART",
                                                "BUFFER/POP"
                                              };

            var luaFilePaths = luaDirs.Select(x => $"{path}/{x}/")
                                        .Where(x => Directory.Exists(x))
                                        .SelectMany(x => Directory.EnumerateFiles(x, "*.lua"))
                                        .ToList();

            var endRequire = "require 'xlua.inner_def_init'";

            var luaenv = new LuaEnv();

            luaenv.DoString(startRequire);

            foreach (var luapath in luaFilePaths)
            {
                var luaTableName = Path.GetDirectoryName(luapath)
                                   .Replace(path, "")
                                   .Trim(Path.DirectorySeparatorChar)
                                   .Replace(Path.DirectorySeparatorChar, '.')
                                   .ToUpper();

                var rawLines = File.ReadLines(luapath).ToList();

                string convertText = luaTableName + "." + Path.GetFileNameWithoutExtension(luapath) + "={";

                int isInFunc = 0;
                int isInIF = 0;
                for (int i=0; i< rawLines.Count(); i++ )
                {
                    string curr = rawLines[i].Trim();
                    if(curr == "")
                    {
                        convertText += curr + "\n";
                        continue;
                    }

                    if (curr.EndsWith(",")
                        || curr.EndsWith("{")
                        || curr.EndsWith("="))
                    {
                        convertText += curr + "\n";
                        continue;
                    }

                    if (curr.Contains("function"))
                    {
                        convertText += curr + "\n";
                        isInFunc++;
                        continue;
                    }

                    if (curr.Contains("if") || curr.Contains("for"))
                    {
                        convertText += curr + "\n";
                        isInIF++;
                        continue;
                    }

                    if (curr.Contains("end"))
                    {
                        if(isInIF != 0)
                        {
                            convertText += curr + "\n";
                            isInIF--;
                            continue;
                        }

                        convertText += curr + "," + "\n";
                        isInFunc--;
                        continue;
                    }


                    if(isInFunc != 0)
                    {
                        convertText += curr + "\n";
                        continue;
                    }

                    convertText += curr + "," + "\n";
                }

                convertText += "\n}";

                luaenv.DoString(convertText, luapath);
            }

            luaenv.DoString(endRequire);
            return luaenv;
        }

        public class Info
        {
            public string name;
            public bool master;
            public string author;
        }

        internal class Content
        {
            internal BackgroundDef backgroundDef;
            internal DepartDef departDef;
            internal PopDef popDef;
            internal EventDef eventDef;
            internal TaskDef taskDef;
            internal BufferDef bufferDef;

            //internal Dictionary<string, GEvent> dictEvent = new Dictionary<string, GEvent>();
            //internal Dictionary<string, DepartDef> dictDepart = new Dictionary<string, DepartDef>();
            //internal Dictionary<string, PopDef> dictPop = new Dictionary<string, PopDef>();
            //internal Dictionary<string, TaskDef> dictTask = new Dictionary<string, TaskDef>();
            //internal Dictionary<string, BufferDef> dictBuffer = new Dictionary<string, BufferDef>();
            //internal Dictionary<string, BackgroundDef> dictBackground = new Dictionary<string, BackgroundDef>();

            internal Dictionary<string, InitSelectDef> dictInitSelect = new Dictionary<string, InitSelectDef>();

            internal Dictionary<string, Dictionary<string, string>> dictlang = new Dictionary<string, Dictionary<string, string>>();
            internal Dictionary<string, PersonName> dictlan2PersonName = new Dictionary<string, PersonName>();

            internal void Load(string mod, LuaEnv luaenv)
            {
                popDef = new PopDef(mod, luaenv.Global);
                departDef = new DepartDef(mod, luaenv.Global);
                backgroundDef = new BackgroundDef(mod,luaenv.Global);
                eventDef = new EventDef(mod, luaenv.Global.Get<LuaTable>("EVENT"));
                taskDef = new TaskDef(mod, luaenv.Global);
                bufferDef = new BufferDef(mod, luaenv.Global.Get<LuaTable>("BUFFER"));
            }
        }

        //private void LoadTask(LuaEnv luaenv)
        //{
        //    LuaTable luaTable = luaenv.Global.Get<LuaTable>("TASK");//映射到LuaTable，by ref

        //    foreach (var key in luaTable.GetKeys<string>())
        //    {
        //        var value = luaTable.Get<TaskDef>(key);
        //        if (value != null)
        //        {
        //            content.dictTask.Add(key, luaTable.Get<TaskDef>(key));
        //        }
        //    }
        //}

        //private void LoadEvent(LuaEnv luaenv)
        //{
        //    LuaTable luaTable = luaenv.Global.Get<LuaTable>("EVENT");//映射到LuaTable，by ref

        //    foreach (var key in luaTable.GetKeys<string>())
        //    {
        //        var value = luaTable.Get<GEvent>(key);
        //        if (value != null)
        //        {
        //            content.dictEvent.Add(key, luaTable.Get<GEvent>(key));
        //        }
        //    }
        //}

        //private void LoadPop(LuaEnv luaenv)
        //{
        //    LuaTable luaTable = luaenv.Global.Get<LuaTable>("POP");//映射到LuaTable，by ref

        //    foreach (var key in luaTable.GetKeys<string>())
        //    {
        //        var value = luaTable.Get<PopDef>(key);
        //        if (value != null)
        //        {
        //            content.dictPop.Add(key, luaTable.Get<PopDef>(key));
        //        }
        //    }
        //}

        //private void LoadDepart(LuaEnv luaenv)
        //{

        //}

        //private void LoadBackground(LuaEnv luaenv)
        //{
        //    LuaTable luaTable = luaenv.Global.Get<LuaTable>("BACKGROUND");//映射到LuaTable，by ref

        //    foreach (var key in luaTable.GetKeys<string>())
        //    {
        //        content.dictBackground.Add(key, luaTable.Get<BackgroundDef>(key).DefaultSet(key));
        //    }
        //}

        private void LoadInitSelectDef(LuaEnv luaenv)
        {
            LuaTable luaTable = luaenv.Global.Get<LuaTable>("INIT_SELECT");//映射到LuaTable，by ref

            foreach (var key in luaTable.GetKeys<string>())
            {
                var value = luaTable.Get<InitSelectDef>(key);
                if(value != null)
                {
                    content.dictInitSelect.Add(key, value);
                }
                
            }
        }

        //internal dynamic Copy(dynamic pythObj)
        //{
        //    return engine.Operations.InvokeMember(scope.GetVariable("copy"), "deepcopy", pythObj);
        //}

        private void loadLocalText(IEnumerable<string> dirs)
        {
            foreach (var dir in dirs)
            {
                var dirName = Path.GetFileNameWithoutExtension(dir);
                Debug.Log($"****************load mod {info.name} langue {dirName} *************");

                var dictText = new Dictionary<string, string>();

                foreach (var filePath in Directory.EnumerateFiles(dir, "*.txt"))
                {
                    foreach (var line in File.ReadAllLines(filePath))
                    {
                        if (line.Count() == 0)
                        {
                            continue;
                        }

                        var key = line.Substring(0, line.IndexOf(':'));
                        var value = line.Substring(line.IndexOf(':')+1, line.Count()- line.IndexOf(':') - 1);

                        if (dictText.ContainsKey(key))
                        {
                            throw new ArgumentException($"already have the local string key, line:{line} in file:{filePath}");
                        }

                        dictText.Add(key, value);
                    }
                }

                content.dictlang.Add(dirName, dictText);

                content.dictlan2PersonName.Add(dirName, PersonName.Generate(dir));
            }
        }

        //internal static IEnumerable<GEvent> GenerateEvent()
        //{
            //Debug.Log("Generate start ");

            //foreach(var mod in Mod.listMod.Where(x => x.content != null))
            //{
            //    foreach (var gevent in mod.content.dictEvent.Values)
            //    {
            //        if(Tools.GRandom.isOccur(gevent.occur_rate()*100))
            //        {
            //            yield return gevent;
            //        }
            //        //if (gevent is GEventDepart)
            //        //{
            //        //    var departEvnet = gevent as GEventDepart;
            //        //    foreach (var depart in GMData.inst.listDepart)
            //        //    {
            //        //        departEvnet.setDepart(depart.def.pyObj);
            //        //        if (departEvnet.isTrigger())
            //        //        {
            //        //            yield return departEvnet;
            //        //        }
            //        //    }
            //        //}
            //        //if (gevent is GEventPop)
            //        //{
            //        //    var popEvent = gevent as GEventPop;
            //        //    foreach (var pop in GMData.inst.listPop)
            //        //    {
            //        //        popEvent.setDepart(pop.def.pyObj);
            //        //        if (popEvent.isTrigger())
            //        //        {
            //        //            yield return popEvent;
            //        //        }
            //        //    }
            //        //}
            //        //else if (gevent.isTrigger())
            //        //{
            //        //    yield return gevent;
            //        //}
            //    }

            //    Debug.Log("Generate finish ");
            //}
            

        //}

        //internal dynamic CreatePythonObj(dynamic parent, string pyType, string name)
        //{
        //    var type = scope.GetVariable(pyType);

        //    var obj = engine.Operations.CreateInstance(type);

        //    engine.Operations.SetMember(parent, name, obj);

        //    return obj;
        //}

        //internal static void SetData(GMData inst)
        //{
        //    foreach (var mod in listMod)
        //    {

        //        var gmPy = mod.scope.GetVariable("gm");

        //        engine.Operations.SetMember(gmPy, "date",    inst.date);
        //        engine.Operations.SetMember(gmPy, "economy", inst.economy);

        //        Func<string, double> collectTaxExpect = (level) => { return inst.collectTaxExpect(level); };
        //        engine.Operations.SetMember(gmPy, "collect_tax_expect", collectTaxExpect);

        //        Action<string> collectTaxStart = (level) => {inst.collectTaxStart(level); };
        //        engine.Operations.SetMember(gmPy, "collect_tax_start", collectTaxStart);

        //        Func<double> collectTaxFinish = () => { return inst.collectTaxFinish(); };
        //        engine.Operations.SetMember(gmPy, "collect_tax_finish", collectTaxFinish);

        //        var departsPy = mod.CreatePythonObj(gmPy, "EmptyClass", "departs");
        //        foreach (var depart in inst.listDepart)
        //        {
        //            engine.Operations.SetMember(departsPy, depart.def.key, depart.def.pyObj);
        //            engine.Operations.SetMember(depart.def.pyObj, "name", depart.def.key);

        //            Func<double> getCropGrowing = () => { 
        //                    return depart.cropGrowing.Value; 
        //                    };
        //            engine.Operations.SetMember(depart.def.pyObj, "get_crop_growing", getCropGrowing);

        //            var popsPy = mod.CreatePythonObj(depart.def.pyObj, "EmptyClass", "pops");
        //            foreach (var pop in depart.pops)
        //            {
        //                if (pop.family != null)
        //                {
        //                    engine.Operations.SetMember(pop.def.pyObj, "family", pop.family);
        //                }

        //                if(pop.consume == null)
        //                {
        //                    engine.Operations.SetMember(pop.def.pyObj, "is_consume", false);
        //                }
        //                else
        //                {
        //                    Func<double> getConsume = () => {
        //                        return pop.consume.Value;
        //                    };
        //                    engine.Operations.SetMember(pop.def.pyObj, "is_consume", true);
        //                    engine.Operations.SetMember(pop.def.pyObj, "get_consume", getConsume);
        //                }

        //                engine.Operations.SetMember(popsPy, pop.def.key, pop.def.pyObj);
        //            }
        //        }

        //        var tasksPy = mod.CreatePythonObj(gmPy, "EmptyClass", "tasks");
        //        foreach(var task in TaskDef.Enumerate())
        //        {
        //            engine.Operations.SetMember(tasksPy, task.key, task.pyObj);

        //            Func<bool> isExit = () =>
        //            {
        //                return inst.listTask.Any(x=>x.def == task);
        //            };

        //            engine.Operations.SetMember(task.pyObj, "isExist", isExit);
        //        }
        //    }
        //}

        //internal static void setInitData(dynamic initdata)
        //{
        //    foreach (var mod in listMod)
        //    {

        //        var initPy = mod.scope.GetVariable("init_data");

        //        //mod.CreateEmptyPythonObj(initPy, "taishou");
        //        engine.Operations.SetMember(initPy, "taishou", initdata);

        //        //foreach (var depart in inst.listDepart)
        //        //{
        //        //    engine.Operations.SetMember(departsPy, depart.def.key, depart.def.pyObj);

        //        //    var popsPy = mod.CreateEmptyPythonObj(depart.def.pyObj, "pops");
        //        //    foreach (var pop in depart.pops)
        //        //    {
        //        //        if (pop.family != null)
        //        //        {
        //        //            engine.Operations.SetMember(popsPy, "family", pop.family);
        //        //        }

        //        //        engine.Operations.SetMember(popsPy, pop.def.key, pop.def.pyObj);
        //        //    }
        //        //}
        //    }
        //}

        //internal void AddBuffersPyObj(BaseDef def, List<Buffer> buffers)
        //{
        //    var buffersPy = def.mod.CreatePythonObj(def.pyObj, "EmptyClass", "buffers");

        //    Action<string> actAdd = (buffName) =>
        //    {
        //        buffers.Add(new Buffer(BufferDef.Find(buffName), def));
        //    };

        //    Mod.engine.Operations.SetMember(buffersPy, "add", actAdd);

        //    Func<string, bool> funcContains = (buffName) =>
        //    {
        //        return buffers.Any(x => x.def.key == buffName);
        //    };

        //    Mod.engine.Operations.SetMember(buffersPy, "contains", funcContains);

        //    Action<string> funcRemove = (buffName) =>
        //    {
        //        buffers.RemoveAll(x => x.def.key == buffName);
        //    };

        //    Mod.engine.Operations.SetMember(buffersPy, "remove", funcRemove);
        //}
    }
}
