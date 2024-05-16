using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    public float moveSpeed;
    private string currentState;

    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    public float jumpForce;

    // References
    public Rigidbody2D rb;
    public Animator animator;
    public bool canJump;
    PlayerInteraction playerInteraction;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        InputManagement();
        
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        if(Input.GetKeyDown(KeyCode.O))
            {
                Roll();
            }
             
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
        }

        if(Input.GetKeyDown(KeyCode.Space) && canJump)
    {
        rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
    }
      
    }

    void Move()
    {
        // Movement chỉ enable sau khi hết hiệu ứng sau khi bị knockback
        if (playerInteraction.KnockbackCounter <= 0)
        {
            rb.velocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            if (playerInteraction.KnockedFromRight)
            {
                rb.velocity = new Vector2(-playerInteraction.KnockbackForceX, playerInteraction.KnockbackForceY);
            }
            else
            {
                rb.velocity = new Vector2(playerInteraction.KnockbackForceX, playerInteraction.KnockbackForceY);
            }
            playerInteraction.KnockbackCounter -= Time.deltaTime;
        }
    }
    void Roll()
    {
        animator.SetTrigger("Roll");
    }

    void Jump()
    {
        animator.SetTrigger("Jump");
    }
    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
