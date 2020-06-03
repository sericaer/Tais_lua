using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TaisEngine;

public class TaskElement : MonoBehaviour
{
    public Task gmTask;

    public LocalText name;
    public Text percent;

    public void onClick()
    {
        if (gmTask.def.name == "COLLECT_TAX")
        {
            GetComponentInParent<sceneMain>().CreateTaskCollectTaxReport();
        }
    }

    // Use this for initialization
    void Start()
    {
        name.format = gmTask.def.name;
    }

    // Update is called once per frame
    void Update()
    {
        percent.text = $"{gmTask.curr_percent}%";
    }
}
