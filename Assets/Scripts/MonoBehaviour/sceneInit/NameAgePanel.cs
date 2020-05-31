using System;
using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class NameAgePanel : MonoBehaviour
{

    public InputField name;
    public Text age;


    public void onNameRandom()
    {
        name.text = TaisEngine.PersonName.EnumFamily().OrderBy(x => Guid.NewGuid()).First()
                  + TaisEngine.PersonName.EnumGiven().OrderBy(x => Guid.NewGuid()).First();

        age.text = ageValue.ToString();
    }

    public void onAgeInc()
    {
        
        ageValue += 5;

        if (ageValue > TaisEngine.InitDataTaishou.ageRange.max)
        {
            ageValue = TaisEngine.InitDataTaishou.ageRange.max;
        }

        age.text = ageValue.ToString();
    }

    public void onAgeDec()
    {
        ageValue -= 5;

        if(ageValue < TaisEngine.InitDataTaishou.ageRange.min)
        {
            ageValue = TaisEngine.InitDataTaishou.ageRange.min;
        }

        age.text = ageValue.ToString();
    }

    public void onConfirm()
    {
        TaisEngine.InitData.inst.taishou.name = name.text;
        TaisEngine.InitData.inst.taishou.age = ageValue;

        gameObject.SetActive(false);

        var select1st = TaisEngine.Mod.EnumerateInitSelect().Single(x => x.is_first);
        GetComponentInParent<sceneInit>().CreateSelectPanel(select1st);
    }

    // Use this for initialization
    void Start()
    {
        onNameRandom();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int ageValue = 35;
}
