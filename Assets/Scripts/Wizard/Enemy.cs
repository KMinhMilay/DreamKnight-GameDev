using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float dmg;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject exposionFX;

    private bool isDie = false;
    public GameObject ExplosionFx => exposionFX;

    private void OnEnable()
    {
        isDie = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDie) return;
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.takeDamageFromEnemyAttack(dmg);
                Die();
                
                if (exposionFX != null)
                {
                    var obj = ObjectPoolManager.SpawnAutoUnSpawn(exposionFX, 1f);
                    obj.transform.position = other.ClosestPoint(transform.position);
                    obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    obj.SetActive(true);
                }
            }
        }
    }
    private void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger(AnimationID.DieAnim);
        }

        isDie = true;
        
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
