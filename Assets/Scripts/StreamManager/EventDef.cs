using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XLua;

namespace TaisEngine
{
    public class EventDef : BaseDef<EventDef.Interface>
    {
        public EventDef(string mod, LuaEnv luaenv) : base(luaenv, mod, "EVENT_DEF")
        {
        }

        [CSharpCallLua]
        public interface Interface
        {
            double occur_rate();
            string title();
            string desc();

            Dictionary<string, Option> options { get; }

            //internal static IEnumerable<GEvent> Enumerate()
            //{
            //    foreach (var mod in Mod.listMod)
            //    {
            //        foreach (var gevent in mod.dictEvent.Values)
            //        {
            //            yield return gevent;
            //        }
            //    }
            //}

            //internal string title
            //{
            //    get
            //    {
            //        var rslt = $"{key}_TITLE";
            //        if (_getTitle != null)
            //        {
            //            var titleList = _getTitle();

            //            rslt = string.Format(titleList[0].ToString(), titleList.Skip(1).ToArray());
            //        }
            //        return rslt;
            //    }
            //}

            //internal string desc
            //{
            //    get
            //    {
            //        var rslt = $"{key}_DESC";
            //        if (_getDesc != null)
            //        {
            //            var descList = _getDesc();

            //            rslt = string.Format(descList[0].ToString(), descList.Skip(1).ToArray());
            //        }
            //        return rslt;
            //    }
            //}

            //internal List<Option> options = new List<Option>();

            //internal bool isTrigger()
            //{
            //    if (_isTrigger == null)
            //        return false;

            //    return _isTrigger();
            //}

            //public GEvent(string name, object pyObj, Mod mod) : base(name, pyObj, mod)
            //{
            //    Mod.engine.Operations.TryGetMember(pyObj, "title", out _getTitle);
            //    Mod.engine.Operations.TryGetMember(pyObj, "desc", out _getDesc);
            //    Mod.engine.Operations.TryGetMember<Func<bool>>(pyObj, "isTrigger", out _isTrigger);

            //    var optNames = Mod.engine.Operations.GetMemberNames(pyObj).Where(x => x.StartsWith("OPTION_")).ToArray();

            //    for (int i = 0; i < optNames.Count(); i++)
            //    {
            //        string optName = $"OPTION_{i + 1}";
            //        if (!Mod.engine.Operations.ContainsMember(pyObj, optName))
            //        {
            //            throw new Exception($"event {name} have not option {optName}");
            //        }

            //        var opType = Mod.engine.Operations.GetMember(pyObj, optName);
            //        options.Add(new Option(optName, this, Mod.engine.Operations.CreateInstance(opType)));
            //    }

            //    mod.dictEvent.Add(name, this);
            //}

            //internal static IEnumerable<GEvent> Generate()
            //{
            //    Debug.Log("Generate start ");
            //    foreach (var gevent in GEvent.Enumerate())
            //    {
            //        if (gevent is GEventDepart)
            //        {
            //            var departEvnet = gevent as GEventDepart;
            //            foreach (var depart in GMData.inst.listDepart)
            //            {
            //                departEvnet.setDepart(depart.def.pyObj);
            //                if (departEvnet.isTrigger())
            //                {
            //                    yield return departEvnet;
            //                }
            //            }
            //        }
            //        if (gevent is GEventPop)
            //        {
            //            var popEvent = gevent as GEventPop;
            //            foreach (var pop in GMData.inst.listPop)
            //            {
            //                popEvent.setDepart(pop.def.pyObj);
            //                if (popEvent.isTrigger())
            //                {
            //                    yield return popEvent;
            //                }
            //            }
            //        }
            //        else if (gevent.isTrigger())
            //        {
            //            yield return gevent;
            //        }
            //    }

            //    Debug.Log("Generate finish ");

            //}

            //private Func<IList<object>> _getTitle;
            //private Func<IList<object>> _getDesc;
            //private Func<bool> _isTrigger;

            //private ScriptScope scope;
        }

        [CSharpCallLua]
        public interface Option
        {
            string desc();
            void selected();
        }

        internal static IEnumerable<EventDef.Interface> Generate()
        {
            Debug.Log("Generate start ");

            foreach (var eventDef in all)
            {
                foreach (var gevent in eventDef.dict.Values)
                {
                    if (Tools.GRandom.isOccur(gevent.occur_rate() * 100))
                    {
                        yield return gevent;
                    }
                    //if (gevent is GEventDepart)
                    //{
                    //    var departEvnet = gevent as GEventDepart;
                    //    foreach (var depart in GMData.inst.listDepart)
                    //    {
                    //        departEvnet.setDepart(depart.def.pyObj);
                    //        if (departEvnet.isTrigger())
                    //        {
                    //            yield return departEvnet;
                    //        }
                    //    }
                    //}
                    //if (gevent is GEventPop)
                    //{
                    //    var popEvent = gevent as GEventPop;
                    //    foreach (var pop in GMData.inst.listPop)
                    //    {
                    //        popEvent.setDepart(pop.def.pyObj);
                    //        if (popEvent.isTrigger())
                    //        {
                    //            yield return popEvent;
                    //        }
                    //    }
                    //}
                    //else if (gevent.isTrigger())
                    //{
                    //    yield return gevent;
                    //}
                }

                Debug.Log("Generate finish ");
            }


        }

        //class Option
        //{
        //    internal string desc
        //    {
        //        get
        //        {
        //            var rslt = $"{_gevent.key}_{_name}_DESC";
        //            if (_getDesc != null)
        //            {
        //                var descList = _getDesc();

        //                rslt = Mod.GetLocalString(rslt, descList.ToArray());
        //            }
        //            return rslt;
        //        }
        //    }

        //    internal Option(string name, GEvent gevent, dynamic pyDef)
        //    {
        //        _name = name;
        //        _pyDef = pyDef;
        //        _gevent = gevent;

        //        if (Mod.engine.Operations.ContainsMember(pyDef, "isValid"))
        //        {
        //            _isValid = Mod.engine.Operations.GetMember<Func<bool>>(pyDef, "isValid");
        //        }

        //        if (Mod.engine.Operations.ContainsMember(pyDef, "desc"))
        //        {
        //            _getDesc = Mod.engine.Operations.GetMember<Func<IList<object>>>(pyDef, "desc");
        //        }

        //        if (Mod.engine.Operations.ContainsMember(pyDef, "selected"))
        //        {
        //            _selected = Mod.engine.Operations.GetMember<Action>(pyDef, "selected");
        //        }
        //    }

        //    internal bool isVaild()
        //    {
        //        if (_isValid == null)
        //            return true;
        //        return _isValid();
        //    }

        //    internal void Selected()
        //    {
        //        _selected?.Invoke();
        //    }

        //    internal dynamic _pyDef;
        //    internal string _name;

        //    private Func<bool> _isValid;
        //    private Func<IList<object>> _getDesc;
        //    private Action _selected;
        //    private GEvent _gevent;
        //}

        //internal class GEventDepart : GEvent
        //{
        //    public GEventDepart(string name, object pyObj, Mod mod) : base(name, pyObj, mod)
        //    {

        //    }

        //    internal void setDepart(dynamic pyObj)
        //    {
        //        Mod.engine.Operations.SetMember(this.pyObj, "depart", pyObj);

        //        foreach (var op in options)
        //        {
        //            Mod.engine.Operations.SetMember(op._pyDef, "depart", pyObj);
        //        }
        //    }
        //}

        //internal class GEventPop : GEvent
        //{
        //    public GEventPop(string name, object pyObj, Mod mod) : base(name, pyObj, mod)
        //    {

        //    }

        //    internal void setDepart(dynamic pyObj)
        //    {
        //        Mod.engine.Operations.SetMember(this.pyObj, "pop", pyObj);

        //        foreach (var op in options)
        //        {
        //            Mod.engine.Operations.SetMember(op._pyDef, "pop", pyObj);
        //        }
        //    }
        //}
    }


}
