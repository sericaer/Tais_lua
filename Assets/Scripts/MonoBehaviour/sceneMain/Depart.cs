
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System;
using ModelShark;

public class Depart : MonoBehaviour
{
    public LocalText name;

    public Text popNum;
    //public Text cropGrowing;

    public GameObject cropGrowing;
    public GameObject popPrefabs;
    public GameObject popContent;

    public GameObject buffPrefabs;
    public GameObject buffContent;

    internal TaisEngine.Depart gmDepart;

    public void OnClickConfirm()
    {
        Destroy(transform.parent.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        name.format = gmDepart.def.name;

        foreach (var pop in gmDepart.pops)
        {
            var gameObject = Instantiate(popPrefabs, popContent.transform);

            var departPop = gameObject.GetComponent<DepartPop>();
            listDepartPops.Add(departPop);

            departPop.gmPop = pop;
            departPop.gameObject.SetActive(false);
        }

        //var cropGrowingToolTip = cropGrowing.transform.GetComponent<TooltipTrigger>();
        //cropGrowingToolTip.funcGetTooltipStr = () =>
        //{
        //    return ("CROP_GROWING",
        //           string.Join("\n", gmDepart.growSpeedDetail.Select(x => $"<color={(x.value < 0 ? "red" : "green")}>{TaisEngine.Mod.GetLocalString(x.name)} {x.value.ToString("N1")} </color>")));
        //};

    }

    // Update is called once per frame
    void Update()
    {
        popNum.text = string.Format("{0:N0}/{1:N0}",
                                    gmDepart.def.pops.Values.Where(x => x.is_tax).Sum(x => x.num),
                                    gmDepart.def.pops.Values.Sum(x => x.num));

        //var cropGrowingValue = cropGrowing.transform.Find("value").GetComponent<Text>();
        //cropGrowingValue.text = "--";
        //if (gmDepart.cropGrowing != null)
        //{
        //    cropGrowingValue.text = gmDepart.cropGrowing.Value.ToString("N1");
        //}

        foreach (var pop in listDepartPops)
        {
            pop.gameObject.SetActive(pop.gmPop.def.num > 0);
        }

        UpdateBuffers();
    }

    private void UpdateBuffers()
    {
        //var needDestroys = listBufferPanels.Where(x => !gmDepart.buffers.Contains(x.gmBuffer)).ToArray();
        //foreach (var elem in needDestroys)
        //{
        //    Destroy(elem.gameObject);

        //    listBufferPanels.Remove(elem);
        //}

        //var needCreate = gmDepart.buffers.Where(x => listBufferPanels.All(y => y.gmBuffer != x)).ToArray();
        //foreach (var elem in needCreate)
        //{
        //    var taskObj = Instantiate(buffPrefabs, buffContent.transform);

        //    taskObj.name = elem.def.key;
        //    taskObj.GetComponent<BufferPanel>().gmBuffer = elem;

        //    listBufferPanels.Add(taskObj.GetComponent<BufferPanel>());
        //}
    }

    private List<DepartPop> listDepartPops = new List<DepartPop>();
    //private List<BufferPanel> listBufferPanels = new List<BufferPanel>();
}
