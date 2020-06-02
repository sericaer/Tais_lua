using System;
using System.IO;
using System.Collections.Generic;
using XLua;

namespace TaisEngine
{
    [CSharpCallLua]
    public interface PopDef
    {
        string name { get; }
        double num { get; set; }
        bool is_tax { get; }

        DepartDef depart();
    }

}
