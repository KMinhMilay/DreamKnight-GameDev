using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Interaction : MonoBehaviour
{
    [SerializeField] MonoBehaviour script;
    
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            print("Nhận sát thương từ đánh thường của player");
            ReceiveDMG(1);
        }
        if (collision.gameObject.tag == "PlayerSkillAttack")
        {
            print("Nhận sát thương từ skill của player");
            ReceiveDMG(3);
        }
    }

    void Update()
    {
        
    }
    void ReceiveDMG(int dmg)
    {
        if (script is GoblinMovement goblin)
        {
            goblin.TakeDamage(dmg);
        }
        if (script is MushroomMovement mushroom)
        {
            mushroom.TakeDamage(dmg);
        }
    }
}
