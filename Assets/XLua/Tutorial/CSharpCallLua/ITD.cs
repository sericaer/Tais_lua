using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace Tutorial
{
    [CSharpCallLua]
    public interface ItfD
    {
        string name { get; set; }
        int f1 { get; set; }
        int f2 { get; set; }
        bool br { get; set; }
        int add(int a, int b);
    }
}
