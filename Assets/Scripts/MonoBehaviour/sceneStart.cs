﻿using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneStart : MonoBehaviour
{
    public GameObject modSelectPanl;
    public GameObject saveSelectPanl;

    public void onNew()
    {
        //TaisEngine.GMData.New("native.SHIZU");
        //TaisEngine.Mod.SetData(TaisEngine.GMData.inst);

#if UNITY_EDITOR_OSX
        TaisEngine.GMData.New(TaisEngine.InitData.Random());
        //aisEngine.Mod.SetData(TaisEngine.GMData.inst);

        SceneManager.LoadScene("sceneMain");
#else
        SceneManager.LoadScene("sceneInit");
#endif

    }

    public void onLoad()
    {
        saveSelectPanl.SetActive(true);
    }

    public void OnMod()
    {
        modSelectPanl.SetActive(true);
    }

    public void onQuit()
    {
        Application.Quit();
    }
}
