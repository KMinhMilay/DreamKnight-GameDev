using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float AttackDamage = 30f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteraction playerInteraction = collision.GetComponent<PlayerInteraction>();
        if (playerInteraction != null)
        {
            playerInteraction.takeDamageFromEnemyAttack(AttackDamage);
        }
    }
}
