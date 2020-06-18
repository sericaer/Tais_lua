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

    TaisEngine.Economy economy;
    TaisEngine.Chaoting chaoting;

    public void LocalCurrTaxSliderValueChanged(float value)
    {
        changedCurrTax = value - (float)TaisEngine.GMData.inst.economy.currTax;
    }

    public void OnConfirm()
    {
        TaisEngine.GMData.inst.economy.currTax += changedCurrTax;
        this.gameObject.SetActive(false);

        Timer.unPause();
    }

    private float changedCurrTax;

    private void Start()
    {
        Timer.Pause();

        economy = TaisEngine.GMData.inst.economy;
        chaoting = TaisEngine.GMData.inst.chaoting;

        LocalCurrTaxSlider.minValue = 0;
        ChaotingExpectTaxSlider.minValue = 0;
        changedCurrTax = 0;

        ChaotingExpectTaxSlider.interactable = false;

        RefreshData();

    }

    private void RefreshData()
    {
        ChaotingExpectTaxSlider.maxValue = (float)chaoting.max_tax;
        ChaotingExpectTaxSlider.value = (float)chaoting.expect_tax;

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

    private void Update()
    {
        LocalCurrTaxText.text = LocalCurrTaxSlider.value.ToString();
    }

}
