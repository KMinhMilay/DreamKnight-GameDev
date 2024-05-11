using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    public float moveSpeed;
    public bool canJump;
   
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
     public float jumpForce;

    // References
    Rigidbody2D rb;
    public Animator animator;
    private DiamondScore diamondScore;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        diamondScore = GameObject.Find("DiamondScore").GetComponent<DiamondScore>();
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
    private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log("Collision detected with: " + collision.gameObject.name); // Thêm dòng này để kiểm tra xem collision có xảy ra không

    if(collision.gameObject.tag == "Ground")
    {
        canJump = true;
    }
    else if(collision.gameObject.tag == "Diamond")
    {
        diamondScore.AddCoins(100); // Thêm điểm khi va chạm với diamond
        Destroy(collision.gameObject); // Xóa diamond khỏi scene
    }
}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump=false;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
    }
    void Roll()
    {
        animator.SetTrigger("Roll");
    }

    void Jump()
    {
       
        
         animator.SetTrigger("Jump");
    }
     
   

   

    

   
}
