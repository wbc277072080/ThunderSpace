using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrounScroller : MonoBehaviour
{
    [SerializeField] Vector2 scrollVelocity;
    Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += scrollVelocity * Time.deltaTime;
    }
}
