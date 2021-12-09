using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    
    Canvas canvas;
    [SerializeField] Image fillImageBack;
    [SerializeField] Image fillImageFront;
    [SerializeField] float fillSpeed = 0.1f;
    [SerializeField] bool delayFill = true;
    [SerializeField] float fillDelayTime = 0.5f;
    float t;
    WaitForSeconds waitForDelayFill;
    float curFillAmount;
    protected float targetFillAmount;

    Coroutine bufferdFillCoroutine;
    
    void Awake(){
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        waitForDelayFill = new WaitForSeconds(fillDelayTime);
    }

    void OnDisable(){
        StopAllCoroutines();
    }

    public virtual void Init(float curValue,float maxValue){
        curFillAmount = curValue / maxValue;
        targetFillAmount = curFillAmount;
        fillImageBack.fillAmount = curFillAmount;
        fillImageFront.fillAmount = curFillAmount;
    }

    public void UpdateStats(float curValue, float maxValue){
        targetFillAmount = curValue / maxValue;
        if(bufferdFillCoroutine !=null){
            StopCoroutine(bufferdFillCoroutine);
        }
        //if stats reduce
        //fill image front = target fill
        //slowly reduce fill image back
        if(curFillAmount > targetFillAmount){
            fillImageFront.fillAmount = targetFillAmount;
            bufferdFillCoroutine = StartCoroutine(BufferdFillCoroutine(fillImageBack));
            return;
        }

        //if stats increase
        //fill image back = target fill
        //slowly increase fill image front
         else if(curFillAmount < targetFillAmount){
            fillImageBack.fillAmount = targetFillAmount;
            bufferdFillCoroutine = StartCoroutine(BufferdFillCoroutine(fillImageFront));
        }
    }


    protected virtual IEnumerator BufferdFillCoroutine(Image image){

        if(delayFill){
            yield return waitForDelayFill;
        }

        t=0f;
        while(t<1f){
            t+=Time.deltaTime * fillSpeed;
            curFillAmount = Mathf.Lerp(curFillAmount,targetFillAmount,t);
            image.fillAmount = curFillAmount;

            yield return null;
        }
        
    }
}

