using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerInput input;

    [SerializeField] float moveSpeed = 10f;

    new Rigidbody2D rigidbody;
    void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        input.onMove += Move;
        input.onStopMove += StopMove;
    }

     void OnDisable() {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }

    void Start()
    {
        rigidbody.gravityScale = 0f;
        input.EnableGameplayInput();
    }

    // Update is called once per frame
    void Move(Vector2 moveInput){
        //Vector2 moveAmount = moveInput * moveSpeed;
        rigidbody.velocity = moveInput * moveSpeed;
    }

    void StopMove(){
        rigidbody.velocity = Vector2.zero;
    }
}
