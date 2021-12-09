using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{   
    [Header("Health")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] protected float maxHealth;
    //current health
    protected float health;

    protected virtual void OnEnable(){
        health = maxHealth;
    }

    public virtual void TakeDamage(float damage){
        health -= damage;

        if(health <= 0f){
            Die();
        }
    }

    public virtual void Die(){
        health = 0f;
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }

    public virtual void RestoreHealth(float value){
        if(health == maxHealth) return;
        
        // health += value;
        // health = Mathf.Clamp(health,0f,maxHealth);
        health = Mathf.Clamp(health + value,0f,maxHealth);

    }

    //automatically restore health by time
    protected IEnumerator HealthRegenerateCoroutine(WaitForSeconds waitTime, float percent)
    {
        while (health < maxHealth)
        {
            yield return waitTime;

            RestoreHealth(maxHealth * percent);
        }
    }

    //automatically damage health by time
    protected IEnumerator DamageOverTimeCoroutine(WaitForSeconds waitTime, float percent)
    {
        while (health > 0f)
        {
            yield return waitTime;

            TakeDamage(maxHealth * percent);
        }
    }

}
