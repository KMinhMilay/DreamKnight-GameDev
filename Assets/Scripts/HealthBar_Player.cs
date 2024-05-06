using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    float maxHealth=100;
    public Image healthBar;
    float lerpSpeed;
    void Start()
    {
        
        health=maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(health>maxHealth) health = maxHealth;
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
    }
    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,health/maxHealth,lerpSpeed);
    }
    public void Damage(float damagePoints)
    {
        if(health > 0)
        {
            Console.WriteLine("Đã nhận damage. Trừ máu");
            health -= damagePoints;
        }
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
        {
            Console.WriteLine("Đã phục hồi máu");
            health += healingPoints;
        }
    }
}
