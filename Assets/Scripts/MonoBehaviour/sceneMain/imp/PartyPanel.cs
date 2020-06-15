using UnityEngine;
using System.Collections;
using TaisEngine;
using UnityEngine.UI.Extensions;

public class PartyPanel : MonoBehaviour
{
    public LocalText label;
    public GameObject inPowerFlag;

    internal Party gmParty;

    // Use this for initialization
    void Start()
    {
        label.format = gmParty._background;
    }

    // Update is called once per frame
    void Update()
    {
        if(gmParty.is_power)
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f);
            inPowerFlag.SetActive(true);
        }
        else if(gmParty.is_first_select)
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1.25f);
            inPowerFlag.SetActive(false);
        }
    }
}
