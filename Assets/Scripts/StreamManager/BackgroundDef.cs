using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace TaisEngine
{
    [CSharpCallLua]
    public interface BackgroundDef
    {
        string name { get; set; }
    }

    static class ExtendBackground
    {
        public static BackgroundDef DefaultSet(this BackgroundDef luaItf, string name)
        {
            luaItf.name = name;
            return luaItf;
        }
    }
}
