using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI.Extensions;

public class SelectPanel : MonoBehaviour
{
    public LocalText textDesc;
    public List<Button> btns;

    internal TaisEngine.InitSelectDef initSelectDef;

    // Use this for initialization
    void Start()
    {
        textDesc.format = initSelectDef.desc();

        TaisEngine.SelectOptionDef[] options = { initSelectDef.OPTION_1,
                                              initSelectDef.OPTION_2,
                                              initSelectDef.OPTION_3,
                                              initSelectDef.OPTION_4,
                                              initSelectDef.OPTION_5,
                                              initSelectDef.OPTION_6,
                                              initSelectDef.OPTION_7,
                                              initSelectDef.OPTION_8,
                                              initSelectDef.OPTION_9,
                                              initSelectDef.OPTION_10,
                                            };

        for (int i = 0; i < options.Count(); i++)
        {
            var opt = options[i];
            if(opt == null)
            {
                break;
            }

            var btn = btns[i];

            btn.gameObject.SetActive(true);

            btn.GetComponentInChildren<LocalText>().format = opt.desc();

            btn.onClick.AddListener(() =>
            {
                opt.selected();

                Destroy(this.gameObject);

                var next = opt.next_select();
                if (next != "")
                {
                    //GetComponentInParent<sceneInit>().CreateSelectPanel(next);
                }
                else
                {
                    GetComponentInParent<sceneInit>().CreateReportPanel();
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
