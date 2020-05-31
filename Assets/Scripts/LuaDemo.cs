using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaDemo : MonoBehaviour
{
    LuaEnv luaenv = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");

        luaenv = new LuaEnv();
        luaenv.DoString("require 'xlua.lua_demo'");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
