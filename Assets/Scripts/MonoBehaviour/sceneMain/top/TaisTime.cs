﻿using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class TaisTime : MonoBehaviour
{
    public Text date;

    public Button btnInc;
    public Button btnDec;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        date.text = TaisEngine.GMData.inst.date.ToString();

        if (!Timer.isUserPause)
        {
            btnInc.enabled = Timer.currSpeed < Timer.MaxSpeed;
            btnDec.enabled = Timer.currSpeed > Timer.MinSpeed;
        }
    }

    public void OnPauseStatusChanged(bool isSelected)
    {
        Timer.isUserPause = isSelected;

        btnInc.enabled = !isSelected;
        btnDec.enabled = !isSelected;
    }

    public void OnSpeedInc()
    {
        Timer.currSpeed++;
    }

    public void OnSpeedDec()
    {
        Timer.currSpeed--;
    }
}
