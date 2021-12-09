using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    TrailRenderer trail;

    void Awake(){
        if(moveDirection!= Vector2.left){
            //rotate projectile direction
            transform.rotation = Quaternion.FromToRotation(Vector2.left,moveDirection);
        }

    }
}
