using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TaisEngine;
using ModelShark;

public class DialogCommon : Dialog
{
    public LocalText title;
    public LocalText content;

    public List<Button> btns;

    internal EventDef.Interface gEvent;

    void Start ()
    {
        List<object> eventTitleParams = gEvent.title();
        title.format = Mod.GetLocalString(eventTitleParams[0] as string, eventTitleParams.Skip(1).ToArray());

        List<object> eventDescParams = gEvent.desc();
        content.format = Mod.GetLocalString(eventDescParams[0] as string, eventDescParams.Skip(1).ToArray());

        for (int i = 1; i <= gEvent.options.Count(); i++)
        {
            var btn = btns[i];

            var opt = gEvent.options[$"OPTION_{i}"];
            
            btn.gameObject.SetActive(true);
            //btn.interactable = opt.isVaild();

            List<string> optDescParams = opt.desc();
            btn.GetComponentInChildren<Text>().text = Mod.GetLocalString(optDescParams[0], optDescParams.Skip(1).ToArray());
            btn.GetComponent<TooltipTrigger>().funcGetTooltipStr = () =>
            {
                List<List<object>> toolTipParams = opt.tooltip();

                return (string.Join("\n", toolTipParams.Select(x =>
                {
                    return Mod.GetLocalString(x[0] as string, x.Skip(1).ToArray());
                })), "");
            };

            btn.onClick.AddListener(async () =>
            {
                //opt.Do();
                //var resumeRslt = opt.MakeResume();
                //if(resumeRslt != null)
                //{
                //    foreach(var elem in resumeRslt)
                //    {
                //        elem.owner.getResumeManager().Add(elem.resume);
                //    }
                //}

                //var nextEvent = opt.getNext();
                //if (nextEvent != null)
                //{
                //    GetComponentInParent<sceneMain>().CreateEventDialog(nextEvent);
                //}
                opt.selected();

                Destroy(this.gameObject);

                string next_event = opt.next_event();

                if(next_event != "" && next_event != null)
                {
                    await GetComponentInParent<Timer>().CreateDialog(EventDef.find(next_event));
                }

                //gEvent.DestroyAction?.Invoke();


            });
        }
    }
}
