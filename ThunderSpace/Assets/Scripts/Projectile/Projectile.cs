using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] float damage;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] protected Vector2 moveDirection;

    //audio
    [Header("Audio")]
    [SerializeField] AudioClip hitSFX;
    [SerializeField] float hitSFXVolume = 0.4f;

    protected GameObject target;

    protected virtual void OnEnable(){
        StartCoroutine(MoveDirectly());
    }
    
    IEnumerator MoveDirectly(){
        while(gameObject.activeSelf){
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Character>(out Character character)){
            character.TakeDamage(damage);
            //var contactPoint = collision.GetContact(0);
            PoolManager.Release(hitVFX, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
            AudioManager.Instance.PlaySFX(hitSFX,hitSFXVolume);
            gameObject.SetActive(false);
        }
    }
}
