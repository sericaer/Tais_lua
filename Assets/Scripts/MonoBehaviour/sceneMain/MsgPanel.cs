using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaisEngine;
using UnityEngine.UI.Extensions;
using System;

public class MsgPanel : MonoBehaviour
{
    public GameObject msgElemtPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount == 0)
        {
            for(int i=0; i< GMData.inst.record.Count; i++)
            {
                var gmObj = Instantiate(msgElemtPrefabs, this.transform) as GameObject;
                gmObj.GetComponent<LocalText>().format = GMData.inst.record[i];
                gmObj.transform.SetAsFirstSibling();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AddMessage(string title)
    {
        GMData.inst.record.Add(title);

        var gmObj = Instantiate(msgElemtPrefabs, this.transform) as GameObject;
        gmObj.GetComponent<LocalText>().format = title;
        gmObj.transform.SetAsFirstSibling();
    }
}