using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public delegate void OnHealthZero();
    public static event OnHealthZero HealthZeroEvent;
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
        if (health <= 0)
        {
            if (HealthZeroEvent != null)
            {
                HealthZeroEvent();
            }
        }
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
    }
    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed); ;

    }
    public void Damage(float damagePoints)
    {
        if(health > 0)
        {
            print("Đã nhận damage. Trừ máu");
            health -= damagePoints;
        }
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
        {
            print("Đã phục hồi máu");
            health += healingPoints;
        }
    }
    public void ResetHealth()
    {
        health = maxHealth;
        HealthBarFiller(); // Cập nhật thanh máu khi reset máu về giá trị tối đa
    }
    public float getCurrentHealth()
    {
        return health;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }
}
