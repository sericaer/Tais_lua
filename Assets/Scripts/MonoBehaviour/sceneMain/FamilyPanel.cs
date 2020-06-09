using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using UnityUITable;

public class FamilyPanel : MonoBehaviour
{
    public TaisEngine.Family gmFamily;

    public LocalText background;
    public LocalText attitude;

    public List<TaisEngine.Person> persons = new List<TaisEngine.Person>();

    public GameObject tableColum;

    // Use this for initialization
    void Start()
    {
        background.format = gmFamily.background.name;
        attitude.format = gmFamily.attitude.ToString();
        persons = gmFamily.persons;

        var columns = tableColum.GetComponentsInChildren<HeaderCellContainer>();
        foreach(var col in columns)
        {
            col.columnInfo.columnTitle = TaisEngine.Mod.GetLocalString(col.label.text);
        }

    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
