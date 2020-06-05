using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaisEngine;
using UnityEngine.UI.Extensions;

public class MsgPanel : MonoBehaviour
{
    public GameObject msgElemtPrefabs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GMData.inst.record.Count - transform.childCount; i++)
        {
            var gmObj = Instantiate(msgElemtPrefabs, this.transform) as GameObject;
            gmObj.GetComponent<LocalText>().format = GMData.inst.record[i];
        }
    }
}
