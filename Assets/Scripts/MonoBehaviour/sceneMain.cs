using TaisEngine;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneMain : MonoBehaviour
{
    public GameObject dialogCommon;
    public GameObject reportCollectTax;
    public MsgPanel msgPanel;

    //public FamilyTop familyTop;
    //public GameObject familyContent;

    //// Use this for initialization
    //void Start()
    //{
    //    foreach(var family in TaisEngine.GMData.inst.familys)
    //    {
    //        var familyTopObj = Instantiate(familyTop, familyContent.transform);
    //        familyTopObj.family = family;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        //if (TaisEngine.GMData.inst.gmEnd)
        //{
        //    SceneManager.LoadScene("sceneEnd");
        //}
    }

    internal async UniTask CreateEventDialogAsync(EventDef.Interface eventobj)
    {
        msgPanel.AddMessage(eventobj.title());

        if (eventobj.hide)
        {
            var opt = eventobj.options["OPTION_1"];
            opt.selected();

            var next = opt.next_event();
            if (next != "")
            {
                await CreateEventDialogAsync(EventDef.find(next));
            }
            return;
        }

        var panelDialog = Instantiate(dialogCommon, this.transform) as GameObject;
        panelDialog.GetComponentInChildren<DialogCommon>().gEvent = eventobj;
    }

    internal void CreateTaskCollectTaxReport()
    {
        var panelDialog = Instantiate(reportCollectTax, this.transform) as GameObject;
    }
}
