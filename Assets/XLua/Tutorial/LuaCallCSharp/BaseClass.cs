using UnityEngine;
using System.Collections;
using System;
using XLua;
using System.Collections.Generic;

namespace Tutorial
{
    [LuaCallCSharp]
    public class BaseClass
    {
        public static void BSFunc()
        {
            Debug.Log("Derived Static Func, BSF = " + BSF);
        }

        public static int BSF = 1;

        public void BMFunc()
        {
            Debug.Log("Derived Member Func, BMF = " + BMF);
        }

        public int BMF { get; set; }
    }
}
