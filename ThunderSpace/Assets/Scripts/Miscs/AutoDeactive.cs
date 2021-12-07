using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactive : MonoBehaviour
{
    WaitForSeconds waitLifttime;
    [SerializeField] float lifttime =  3f;
    [SerializeField] bool destroyGameObject;

    void Awake(){
        waitLifttime = new WaitForSeconds(lifttime);
    }

    void OnEnable(){
        StartCoroutine(DeactivateCoroutine());
    } 

    IEnumerator DeactivateCoroutine(){
        yield return waitLifttime;
        if(destroyGameObject){
            Destroy(gameObject);
        }else{
            gameObject.SetActive(false);
        }
    }
}
