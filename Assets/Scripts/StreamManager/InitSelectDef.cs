using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace TaisEngine
{
    //[CSharpCallLua]
    //public delegate string delegateGetString();

    //[CSharpCallLua]
    //public delegate void delegateSelected();

    [CSharpCallLua]
    public interface InitSelectDef
    {
        string desc();

        bool is_first { get; }

        SelectOptionDef OPTION_1 { get; }
        SelectOptionDef OPTION_2 { get; }
        SelectOptionDef OPTION_3 { get; }
        SelectOptionDef OPTION_4 { get; }
        SelectOptionDef OPTION_5 { get; }
        SelectOptionDef OPTION_6 { get; }
        SelectOptionDef OPTION_7 { get; }
        SelectOptionDef OPTION_8 { get; }
        SelectOptionDef OPTION_9 { get; }
        SelectOptionDef OPTION_10 { get; }
    }

    [CSharpCallLua]
    public interface SelectOptionDef
    {
        string desc();

        void selected();

        string next_select();
    }

    //static class ExtendInitSelect
    //{
    //    public static InitSelectDef DefaultSet(this InitSelectDef luaItf, string name)
    //    {
    //        if(luaItf.desc == null)
    //        {
    //            luaItf.desc = () => $"{name}_DESC";
    //        }

    //        (string name, SelectOptionDef value)[] optionLuas = {
    //                                                  ("OPTION_1", luaItf.OPTION_1),
    //                                                  ("OPTION_2", luaItf.OPTION_2),
    //                                                  ("OPTION_3", luaItf.OPTION_3),
    //                                                  ("OPTION_4", luaItf.OPTION_4),
    //                                                  ("OPTION_5", luaItf.OPTION_5),
    //                                                  ("OPTION_6", luaItf.OPTION_6),
    //                                                  ("OPTION_7", luaItf.OPTION_7),
    //                                                  ("OPTION_8", luaItf.OPTION_8),
    //                                                  ("OPTION_9", luaItf.OPTION_9),
    //                                                  ("OPTION_10", luaItf.OPTION_10),
    //                                                };

    //        for (int i = 0; i < optionLuas.Count(); i++)
    //        {
    //            var optLua = optionLuas[i];
    //            if (optLua.value == null)
    //            {
    //                break;
    //            }

    //            if(optLua.value.desc == null)
    //            {
    //                optLua.value.desc = () => $"{name}_{optLua.name}_DESC";
    //            }
    //            if(optLua.value.selected == null)
    //            {
    //                optLua.value.selected = () => { };
    //            }
    //        }

    //        return luaItf;
    //    }
    //}
}
