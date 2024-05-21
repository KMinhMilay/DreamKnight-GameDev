using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    float skillCost;

    //Ref
    public Animator animator;
    public PlayerMovement playerMovement;
    public SwordSlashBehavior swordslash;
    public Transform SwordSlashLauncher;
    public GameObject SwordSlashObject;
    public bool skillOnCooldown;
    [SerializeField] GameObject Mana;
    ManaBar manabar;
    [SerializeField] GameObject Health;
    HealthBar healthbar;


    // Start is called before the first frame update
    void Start()
    {
        manabar = Mana.GetComponent<ManaBar>();
        healthbar = Health.GetComponent<HealthBar>();
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

             if (Input.GetKeyDown(KeyCode.J))
        {
            groundAttack();
        }
        void Attack()
        {
            if (!skillOnCooldown)
            {
                animator.SetTrigger("Attack");
            }
            
        }
        void Skill()
        {
            skillCost = 20;
            if (manabar.getCurrentMana() > skillCost && !skillOnCooldown)
            {
                animator.SetTrigger("Skill");
                manabar.GetMana(skillCost);
            }
            
        }
        void groundAttack()
       
        {
            animator.SetTrigger("groundAttack");
        }
    }
    public void createSwordSlash()
    {
        GameObject projectile = Instantiate(SwordSlashObject, SwordSlashLauncher.position, SwordSlashObject.transform.rotation);
        Vector3 originalScale = projectile.transform.localScale;

        projectile.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? 0.2f : -0.2f,
            originalScale.y,
            originalScale.z
            );
    }
}
