using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public class WizardBullet : MonoBehaviour
{
    [Header("Configs")] 
    [SerializeField] private float bulletSpd = 2.5f;
    [SerializeField] private float timeLive = 5;
    [SerializeField] private string ignoreObjTag;
    [SerializeField] private GameObject exposionFX;

    public GameObject ExplosionFx => exposionFX;
    
    //cached
    //cached
    private Vector2 dir;
    private float curTimeLive;
    private float dmg;
    public void Init(Vector2 targetDir, float dmg)
    {
        dir = targetDir;
        curTimeLive = 0;
        this.dmg = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!String.IsNullOrEmpty(ignoreObjTag) && collider.gameObject.CompareTag(ignoreObjTag))
        {
            return;
        }
        
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerInteraction playerInteraction = collider.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.takeDamageFromEnemyAttack(dmg);
            }
        }
        if (exposionFX != null)
        {
            var obj = ObjectPoolManager.SpawnAutoUnSpawn(exposionFX, 1f);
            obj.transform.position = collider.ClosestPoint(transform.position);
            obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            obj.SetActive(true);
        }
        
        DestroySelf();
    }
    
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        curTimeLive += Time.deltaTime;
        if (curTimeLive > timeLive)
        {
            DestroySelf();
        }
        else
        {
            transform.position += (Vector3)dir * bulletSpd * Time.deltaTime;
        }
    }
}
