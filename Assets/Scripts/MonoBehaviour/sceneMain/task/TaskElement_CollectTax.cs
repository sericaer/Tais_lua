using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TaisEngine;
using System.Collections.Generic;
using UnityUITable;

public class TaskElement_CollectTax : TaskElement
{
    public Button btnConfirm;

    public Table collectTaxInfo;

    public List<CollectTaxItem> collectTaxItems;

    void Start()
    {
        isConfirmed = false;
    }

    void onClickConfirm()
    {
        isConfirmed = true;
        Destroy(this.gameObject);
    }

    private void Update()
    {
        btnConfirm.gameObject.SetActive(!isConfirmed);
        foreach(var toggle in collectTaxInfo.GetComponentsInChildren<Toggle>())
        {
            toggle.interactable = !isConfirmed;
        }
    }

    private bool isConfirmed;
}

public class CollectTaxItem
{
    string depart_name;
    int level;
    double value;
}
