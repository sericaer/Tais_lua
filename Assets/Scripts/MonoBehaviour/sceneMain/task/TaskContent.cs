using System.Collections.Generic;
using System.Linq;

using TaisEngine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class TaskContent : MonoBehaviour
{
    public static List<TaskElement> elements = new List<TaskElement>();

    public GameObject taskPrefabs;

    void Start()
    {

    }

    void Update()
    {

        var needDestroys = elements.Where(x => !x.gmTask.is_start()).ToArray();
        foreach(var elem in needDestroys)
        {
            Destroy(elem.gameObject);

            elements.Remove(elem);
        }

        var needCreate = TaisEngine.GMData.inst.listTask.Where(x => x.def.is_start() && elements.All(y=>y.gmTask != x.def)).ToArray();
        foreach (var elem in needCreate)
        {
            var taskObj = Instantiate(taskPrefabs, this.transform);

            taskObj.name = elem.def.name;
            taskObj.GetComponent<TaskElement>().gmTask = elem.def;

            elements.Add(taskObj.GetComponent<TaskElement>());
        }
    }
}