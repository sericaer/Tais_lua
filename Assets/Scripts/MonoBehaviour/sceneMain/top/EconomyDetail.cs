using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ModelShark;

public class EconomyDetail : MonoBehaviour
{
    public Slider LocalCurrTaxSlider;
    public Text LocalCurrTaxText;

    public Slider ChaotingExpectTaxSlider;
    public Text ChaotingExpectTaxText;

    public Button btnConfirm;

    public TaisEngine.Economy economy;

    public void LocalCurrTaxSliderValueChanged(float value)
    {
        changedCurrTax = value - (float)economy.currTax;
    }

    public void OnConfirm()
    {
        economy.currTax += changedCurrTax;
        this.gameObject.SetActive(false);
    }

    private float changedCurrTax;

    private void Start()
    {
        LocalCurrTaxSlider.minValue = 0;
        ChaotingExpectTaxSlider.minValue = 0;
        changedCurrTax = 0;

        ChaotingExpectTaxSlider.interactable = false;

    }

    private void Update()
    {
        ChaotingExpectTaxSlider.maxValue = (float)TaisEngine.GMData.inst.chaoting.max_tax;
        ChaotingExpectTaxSlider.value = (float)TaisEngine.GMData.inst.chaoting.expect_tax;

        LocalCurrTaxSlider.maxValue = (float)economy.maxTax;
        LocalCurrTaxSlider.value = (float)economy.currTax + changedCurrTax;

        ChaotingExpectTaxText.text = ChaotingExpectTaxSlider.value.ToString();
        LocalCurrTaxText.text = LocalCurrTaxSlider.value.ToString();

        LocalCurrTaxSlider.interactable = economy.local_tax_change_valid;
        if(!LocalCurrTaxSlider.interactable)
        {
            LocalCurrTaxSlider.GetComponent<TooltipTrigger>().funcGetTooltipStr = () =>
            {
                return ("", TaisEngine.Mod.GetLocalString("VALID_DAYS_SPAN", economy.taxChangedDaysSpan, TaisEngine.GMDate.ToString(economy.validTaxChangedDays)));
            };
        }
    }


}
