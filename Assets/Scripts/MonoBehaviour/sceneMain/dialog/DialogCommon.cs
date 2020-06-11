using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TaisEngine;

public class DialogCommon : Dialog
{
    public LocalText title;
    public LocalText content;

    public List<Button> btns;

    internal EventDef.Interface gEvent;

    void Start ()
    {
        title.format = gEvent.title();
        content.format = gEvent.desc();

        for (int i = 1; i <= gEvent.options.Count(); i++)
        {
            var btn = btns[i];

            var opt = gEvent.options[$"OPTION_{i}"];
            
            btn.gameObject.SetActive(true);
            //btn.interactable = opt.isVaild();

            btn.GetComponentInChildren<Text>().text = opt.desc();

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
