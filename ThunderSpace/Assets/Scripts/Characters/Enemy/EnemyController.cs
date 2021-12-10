using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    [Header("Move")]
    [SerializeField] float paddingX;
    [SerializeField] float paddingY;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float moveRotationAngle = 25f;

    //fire
    [Header("Fire")]
    [SerializeField] float minFireInterval;
    [SerializeField] float maxFireInterval;
    [SerializeField] GameObject[] projectiles;
    [SerializeField] Transform muzzle;

    //audio
    [Header("Audio")]
    [SerializeField] AudioClip projectileSFX;
    [SerializeField] float projectileSFXVolume = 0.4f;

    void OnEnable(){
        StartCoroutine(nameof(RandomlyMovingCoroutine));
        StartCoroutine(nameof(RandomlyFireCoroutine));
    }

    void OnDisable(){
        StopAllCoroutines();
    }
    //move
    IEnumerator RandomlyMovingCoroutine(){
        transform.position = Viewport.Instance.RandomEnemySpawnPosition(paddingX, paddingY);

        Vector3 targetPosition = Viewport.Instance.RandomRightHalfPosition(paddingX, paddingY);
        while(gameObject.activeSelf){
            // if has not arrived target postion
            if(Vector3.Distance(transform.position,targetPosition)>= moveSpeed * Time.fixedDeltaTime){
                //keep moving to targetPosition
                transform.position = Vector3.MoveTowards(transform.position,targetPosition,moveSpeed * Time.fixedDeltaTime);
                //make enemy rotate with x axis while moving
                transform.rotation = Quaternion.AngleAxis((targetPosition - transform.position).normalized.y * moveRotationAngle, Vector3.right);


            }else{
                //else
                //set a  new target  position
                targetPosition = Viewport.Instance.RandomRightHalfPosition(paddingX,paddingY);
            }
            
            
            yield return new WaitForFixedUpdate();
        }
    }

    //fire
    IEnumerator RandomlyFireCoroutine(){

        while(gameObject.activeSelf){
            yield return new WaitForSeconds(Random.Range(minFireInterval,maxFireInterval));

            if(GameManager.GameState == GameState.GameOver){
                yield break;
            }

            foreach (var projectile in projectiles)
            {
                PoolManager.Release(projectile,muzzle.position);
            }

            AudioManager.Instance.PlaySFX(projectileSFX,projectileSFXVolume);
        }
    }
}
