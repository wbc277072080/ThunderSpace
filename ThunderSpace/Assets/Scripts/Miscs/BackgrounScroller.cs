using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrounScroller : MonoBehaviour
{
    [SerializeField] Vector2 scrollVelocity;
    Material material;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     material.mainTextureOffset += scrollVelocity * Time.deltaTime;
    // }
    IEnumerator Start(){
        while(GameManager.GameState!= GameState.GameOver){
             material.mainTextureOffset += scrollVelocity * Time.deltaTime;
             yield return null;
        }
    }
}
