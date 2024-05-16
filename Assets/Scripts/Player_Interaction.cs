using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // For Knock back effect
    public float KnockbackForceX;
    public float KnockbackForceY;
    public float KnockbackCounter;
    public float KnockbackTotalTime;

    public bool KnockedFromRight;
    public float flickerDuration;

    public float ActiveAttackDamageReceive;

    // References
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject Health;

    [SerializeField] ManaBar manaBar;
    [SerializeField] GameObject Mana;

    [SerializeField]
    PlayerMovement playerMovement;
    PlayerAnimator playerAnimator;
    Animator animator;
    Renderer Renderer;
    Color Color;

    
    
    // Start is called before the first frame update
    void Start()
    {
        KnockbackForceX = 10f;
        KnockbackForceY = 4f;
        KnockbackTotalTime = 0.1f;

        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();
        Renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();

        healthBar = Health.GetComponent<HealthBar>();
        manaBar = Mana.GetComponent<ManaBar>();

        Color = Renderer.material.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            StartCoroutine("BecomeInvulnerable");

            touchSpikesEvent();
        }
        if (collision.gameObject.tag == "EnemyAttack")
        {
            KnockbackCounter = KnockbackTotalTime;
            KnockedFromRight = collision.transform.position.x >= transform.position.x;
            print("Bị tấn công chủ động!");
            StartCoroutine("BecomeInvulnerable");
        }
        if (collision.gameObject.tag == "HpPotion")
        {
            healthBar.Heal(20); // Hồi 20 máu khi chạm vào HpPotion
            Destroy(collision.gameObject); // Xóa HpPotion khỏi scene
        }
    }
    public void takeDamageFromEnemyAttack(float dmg)
    {
        healthBar.Damage(dmg);
    }

    private void touchSpikesEvent()
    {
        playerMovement.rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
        animator.SetTrigger("Hurt");
        healthBar.Damage(10);
    }
    private void touchEnemyEvent()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Spikes"))
        {
            
        }
    }

    IEnumerator BecomeInvulnerable()
    {
        // Hiệp ứng nhấp nháy ngay sau khi ăn dmg
        flickerDuration = 0.25f;

        //6: Player layer, 3: spike layer, 7: enemy layer, 8: enemy attack zone
        Physics2D.IgnoreLayerCollision(6, 3, true);
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 8, true);
        for (int i = 0; i < 5; i++)
        {
            Color.a = 0.3f;
            Renderer.material.color = Color;
            yield return new WaitForSeconds(flickerDuration);
            Color.a = 0.5f;
            Renderer.material.color = Color;
            yield return new WaitForSeconds(flickerDuration);
        }
        Color.a = 1f;
        Renderer.material.color = Color;

        // set IgnoreLayerCollision về lại false sau khi hết hiệu ứng nhấp nháy
        Physics2D.IgnoreLayerCollision(6, 3, false);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            KnockbackCounter = KnockbackTotalTime;
            KnockedFromRight = collision.transform.position.x >= transform.position.x;
            print("chạm địch!");
            healthBar.Damage(15);
            StartCoroutine("BecomeInvulnerable");
        }
    }
}
