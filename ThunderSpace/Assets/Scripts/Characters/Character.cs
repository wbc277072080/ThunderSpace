using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{   
    [Header("Health")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] protected float maxHealth;

    [SerializeField] StatsBar onHeadHealthBar;
    [SerializeField] bool showOnHeadHealthBar = true;

    //audio
    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float deathSFXVolume = 0.4f;



    //current health
    protected float health;

    protected virtual void OnEnable(){
        health = maxHealth;
        if(showOnHeadHealthBar){
            ShowOnHeadHealthBar();
        }else{
            HideOnHeadHealthBar();
        }
    }

     public void ShowOnHeadHealthBar(){
        onHeadHealthBar.gameObject.SetActive(true);
        onHeadHealthBar.Init(health,maxHealth);
    }

    public void HideOnHeadHealthBar(){
        onHeadHealthBar.gameObject.SetActive(false);
    }

    public virtual void TakeDamage(float damage){
        health -= damage;

        if(showOnHeadHealthBar && gameObject.activeSelf){
            onHeadHealthBar.UpdateStats(health,maxHealth);
        }

        if(health <= 0f){
            Die();
        }
    }

    public virtual void Die(){
        health = 0f;
        AudioManager.Instance.PlaySFX(deathSFX,deathSFXVolume);
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }

    public virtual void RestoreHealth(float value){
        if(health == maxHealth) return;
        
        // health += value;
        // health = Mathf.Clamp(health,0f,maxHealth);
        health = Mathf.Clamp(health + value,0f,maxHealth);
        if(showOnHeadHealthBar){
            onHeadHealthBar.UpdateStats(health,maxHealth);
        }

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
