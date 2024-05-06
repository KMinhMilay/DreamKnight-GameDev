using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(
            Input.GetKeyDown(KeyCode.U))
            {
                Attack();
            }
             if(
            Input.GetKeyDown(KeyCode.I))
            {
                Skill();
            }
            if(
            Input.GetKeyDown(KeyCode.E))
            {
                Pray();
            }
             if (Input.GetKeyDown(KeyCode.J))
        {
            groundAttack();
        }
        void Attack()
        {
            animator.SetTrigger("Attack");
        }
        void Skill()
        {
            animator.SetTrigger("Skill");
        }
         void Pray()
        {
            animator.SetTrigger("Pray");
        }
        void groundAttack()
       
        {
            
            animator.SetTrigger("groundAttack");
        }

        
        
    }
}
