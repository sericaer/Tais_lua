using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;

using ModelShark;

public class BufferPanel : MonoBehaviour
{
    public LocalText title;

    internal TaisEngine.Buffer gmBuffer;

    internal TooltipTrigger tooltipTrigger;

    // Use this for initialization
    void Start()
    {
        title.format = gmBuffer.def.name;

        tooltipTrigger = GetComponent<TooltipTrigger>();
        tooltipTrigger.funcGetTooltipStr = getBuffText;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private (string title, string desc) getBuffText()
    {
        string title = TaisEngine.Mod.GetLocalString(gmBuffer.def.name);

        string desc = "";
        //if(gmBuffer.def.taxEffect != null)
        //{
        //    var effect = gmBuffer.def.taxEffect();

        //    desc += $"<color={(effect <0? "red" : "green")}>" + TaisEngine.Mod.GetLocalString("TAX_EFFECT", effect.ToString("P1")) + "</color> \n";
        //}
        //if (gmBuffer.def.cropGrowingEffect != null)
        //{
        //    var effect = gmBuffer.def.cropGrowingEffect();

        //    desc += $"<color={(effect < 0 ? "red" : "green")}>" + TaisEngine.Mod.GetLocalString("CROP_GROWING_EFFECT", effect.ToString("P1")) + "</color> \n";
        //}
        //if (gmBuffer.def.consumeEffect != null)
        //{
        //    var effect = gmBuffer.def.consumeEffect();

        //    desc += $"<color={(effect < 0 ? "red" : "green")}>" + TaisEngine.Mod.GetLocalString("CONSUME_EFFECT", effect.ToString("P1")) + "</color> \n";
        //}

        return (title, desc);
    }
}
