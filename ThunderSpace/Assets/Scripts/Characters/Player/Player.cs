using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    
    
    //health
    [SerializeField] bool regenerateHealth = true;
    [SerializeField] float healthRegenerateTime;
    [SerializeField, Range(0f,1f)] float healthRegeneratePercent;
    [SerializeField] StatsBar_HUD statsBar_HUD;
    
    //audio
    [Header("Audio")]
    [SerializeField] AudioClip projectileSFX;
    [SerializeField] float projectileSFXVolume = 0.4f;

    // Start is called before the first frame update
    [Header("input")]
    [SerializeField] PlayerInput input;

    //move
    [Header("Move")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float paddingX = 0.2f;
    [SerializeField] float paddingY = 0.2f;
    [SerializeField] float accelerationTime = 0.5f;
    [SerializeField] float decelerationTime = 0.5f;
    [SerializeField] float moveRotationAngle = 50f;

    //fire
    [Header("Fire")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectile1;
    [SerializeField] GameObject projectile2;

    [SerializeField] Transform muzzleMiddle;
    [SerializeField] Transform muzzleTop;
    [SerializeField] Transform muzzleBottom;

    [SerializeField, Range(0,2)] int weaponPower = 0;
    [SerializeField] float fireInterval = 0.2f;

    WaitForSeconds waiteForFireInterval;
    WaitForSeconds waitHealthRegenerateTime;

    new Rigidbody2D rigidbody;

    Coroutine moveCoroutine;
    Coroutine healthRegenerateCoroutine;

    void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable() {
        base.OnEnable();

        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onFire += Fire;
        input.onStopFire +=StopFire;
    }

   

     void OnDisable() {
        input.onMove -= Move;
        input.onStopMove -= StopMove;

        input.onFire -= Fire;
        input.onStopFire -=StopFire;
    }

    void Start()
    {
        rigidbody.gravityScale = 0f;
        waiteForFireInterval  = new WaitForSeconds(fireInterval);
        waitHealthRegenerateTime = new WaitForSeconds(healthRegenerateTime);

        //health
        statsBar_HUD.Init(health,maxHealth);
        input.EnableGameplayInput();
        
    }


    //override
    //take damage
    public override void TakeDamage(float damage){
        base.TakeDamage(damage);
        statsBar_HUD.UpdateStats(health,maxHealth);
        if(gameObject.activeSelf){
            if(regenerateHealth){
                if(healthRegenerateCoroutine!=null){
                    StopCoroutine(healthRegenerateCoroutine);
                }
                healthRegenerateCoroutine = StartCoroutine(HealthRegenerateCoroutine(waitHealthRegenerateTime,healthRegeneratePercent));
            }
        }
    }

    //override
    //restore health
    public override void RestoreHealth(float value){
        base.RestoreHealth(value);
        statsBar_HUD.UpdateStats(health,maxHealth);
    }

    //override
    //die
    public override void Die(){
        GameManager.onGameOver?.Invoke();

        GameManager.GameState = GameState.GameOver;
        statsBar_HUD.UpdateStats(0f,maxHealth);
        base.Die();
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
        StartCoroutine(nameof(MovePositionLimitCoroutine));
    }

    void StopMove(){
        //rigidbody.velocity = Vector2.zero;
        if(moveCoroutine!=null){
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime,Vector2.zero,Quaternion.identity));
        StopCoroutine(nameof(MovePositionLimitCoroutine));
    }

    IEnumerator MovePositionLimitCoroutine(){
        while(true){
            transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position, paddingX, paddingY);

            yield return null;
        }
    }
    IEnumerator MoveCoroutine(float time, Vector2 moveVelovity,Quaternion moveRotation){
        float t = 0f;
        Vector2 previousVelocity = rigidbody.velocity;
        Quaternion previousRotation = transform.rotation;

        while(t < 1f){
            t += Time.fixedDeltaTime / time;
            rigidbody.velocity = Vector2.Lerp(previousVelocity, moveVelovity, t);
            //rotation
            transform.rotation = Quaternion.Lerp(previousRotation, moveRotation, t);

            yield return new WaitForFixedUpdate();
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

            switch (weaponPower)
            {
                case 0:
                    PoolManager.Release(projectile,muzzleMiddle.position);
                    break;
                case 1:
                    PoolManager.Release(projectile1,muzzleTop.position);
                    PoolManager.Release(projectile2,muzzleBottom.position);
                    break;
                case 2:
                    PoolManager.Release(projectile1,muzzleTop.position);
                    PoolManager.Release(projectile,muzzleMiddle.position);
                    PoolManager.Release(projectile2,muzzleBottom.position);
                    break;
                default:
                    break;
            }
            //audio
            AudioManager.Instance.PlaySFX(projectileSFX,projectileSFXVolume);

            //fire interval
            yield return waiteForFireInterval;

            
        }
    }


}
