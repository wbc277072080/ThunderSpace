using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar_HUD : StatsBar
{
    [SerializeField] Text percentText;

    void SetPercentText(){
        percentText.text = Mathf.RoundToInt(targetFillAmount * 100f) + "%";
    }

    public override void Init(float curValue, float maxValue){
        base.Init(curValue,maxValue);
        SetPercentText();
    }

    protected override IEnumerator BufferdFillCoroutine(Image image){
        SetPercentText();
        return base.BufferdFillCoroutine(image);
    }
}
