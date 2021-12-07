using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerInput input;

    //move
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float paddingX = 0.2f;
    [SerializeField] float paddingY = 0.2f;
    [SerializeField] float accelerationTime = 3f;
    [SerializeField] float decelerationTime = 3f;
    [SerializeField] float moveRotationAngle = 50f;

    //fire
    [SerializeField] GameObject projectile;
    [SerializeField] Transform muzzle;
    [SerializeField] float fireInterval = 0.2f;
    WaitForSeconds waiteForFireInterval;

    new Rigidbody2D rigidbody;

    Coroutine moveCoroutine;

    void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onFire += Fire;
        input.onStopFire +=StopFire;
    }

     void OnDisable() {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }

    void Start()
    {
        rigidbody.gravityScale = 0f;
        waiteForFireInterval  = new WaitForSeconds(fireInterval);
        input.EnableGameplayInput();
    }

    //move
    // Update is called once per frame
    void Move(Vector2 moveInput){
        //Vector2 moveAmount = moveInput * moveSpeed;
        //rigidbody.velocity = moveInput * moveSpeed;
        if(moveCoroutine!=null){
            StopCoroutine(moveCoroutine);
        }
        //add rotation angle
        Quaternion moveRotation = Quaternion.AngleAxis(moveRotationAngle * moveInput.y, Vector3.right);

        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime,moveInput.normalized * moveSpeed, moveRotation));
        StartCoroutine(MovePositionLimitCoroutine());
    }

    void StopMove(){
        //rigidbody.velocity = Vector2.zero;
        if(moveCoroutine!=null){
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime,Vector2.zero,Quaternion.identity));
        StopCoroutine(MovePositionLimitCoroutine());
    }

    IEnumerator MovePositionLimitCoroutine(){
        while(true){
            transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position, paddingX, paddingY);

            yield return null;
        }
    }
    IEnumerator MoveCoroutine(float time, Vector2 moveVelovity,Quaternion moveRotation){
        float t = 0f;
        while(t < time){
            t += Time.fixedDeltaTime / time;
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, moveVelovity, t/time);
            //rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, t/time);

            yield return null;
        }
    }

    //fire
    void Fire(){
        StartCoroutine(nameof(FireCoroutine));
        
    }

    void StopFire(){
        StopCoroutine(nameof(FireCoroutine));
    }

    IEnumerator FireCoroutine(){
        
        while(true){
            Instantiate(projectile, muzzle.position, Quaternion.identity);
            //fire interval
            yield return waiteForFireInterval;
        }
    }


}
