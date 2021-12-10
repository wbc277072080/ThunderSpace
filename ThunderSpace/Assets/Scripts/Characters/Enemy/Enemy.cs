using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{   
    [SerializeField] int scorePoint = 100;
    public override void Die(){
        EnemyManager.Instance.RemoveFromList(gameObject);
        ScoreManager.Instance.AddScore(scorePoint);
        base.Die();
    }

    //play die when crash
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.TryGetComponent<Player>(out Player player)){
            player.Die();
            Die();
        }
    }
}
