using System;
using System.IO;
using System.Collections.Generic;
using XLua;

namespace TaisEngine
{
    [CSharpCallLua]
    public interface DepartDef
    {
        string name { get; }
        List<double> color { get; }
        Dictionary<string, PopDef> pops { get; }
    }

}
