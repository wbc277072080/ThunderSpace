using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile_Tracking : Projectile
{
   void Awake(){
       target = GameObject.FindGameObjectWithTag("Player");
   }

   protected override void OnEnable(){
       
       StartCoroutine(nameof(MoveDirectionCoroutine));
       base.OnEnable();
       
   }

   //wait for aimming
    IEnumerator MoveDirectionCoroutine(){
        yield return null;
        if(target.activeSelf){
            moveDirection = (target.transform.position - transform.position).normalized;
        }
    }
}
