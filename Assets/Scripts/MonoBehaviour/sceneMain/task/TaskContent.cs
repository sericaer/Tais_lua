using System.Collections.Generic;
using System.Linq;

using TaisEngine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class TaskContent : MonoBehaviour
{
    public static List<TaskElement> elements;

    public GameObject taskPrefabs;

    void Start()
    {
        elements = new List<TaskElement>();
    }

    void Update()
    {

        var needDestroys = elements.Where(x => !x.gmTask.start).ToArray();
        foreach(var elem in needDestroys)
        {
            Destroy(elem.gameObject);

            elements.Remove(elem);
        }

        var needCreate = TaisEngine.GMData.inst.tasks.Where(x => x.start && elements.All(y=>y.gmTask != x)).ToArray();
        foreach (var elem in needCreate)
        {
            var taskObj = Instantiate(taskPrefabs, this.transform);

            taskObj.name = elem.def.name;
            taskObj.GetComponent<TaskElement>().gmTask = elem;

            elements.Add(taskObj.GetComponent<TaskElement>());
        }
    }
}