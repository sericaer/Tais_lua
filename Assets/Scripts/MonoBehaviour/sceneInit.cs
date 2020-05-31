﻿using System.Collections;
using System.Dynamic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneInit : MonoBehaviour
{
    public GameObject selectPanel;
    public GameObject reportPanel;
    public GameObject nameAgePanel;

    void Start()
    {
        TaisEngine.InitData.Generate();
        CreateNameAgePanel();
    }

    internal void CreateSelectPanel(TaisEngine.InitSelectDef selectDef)
    {
        var panelSelect = Instantiate(selectPanel, this.transform) as GameObject;
        panelSelect.GetComponentInChildren<SelectPanel>().initSelectDef = selectDef;
    }

    internal void CreateReportPanel()
    {
        var panelSelect = Instantiate(reportPanel, this.transform) as GameObject;
    }

    internal void CreateNameAgePanel()
    {
        var panelSelect = Instantiate(nameAgePanel, this.transform) as GameObject;
    }
}
