using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyingEyeMovement : MonoBehaviour
{
    private int damageCount = 0; // Biến đếm số lần nhận sát thương
    public int deathThreshold = 3; // Số lần nhận sát thương để kích hoạt animation death

    public float moveSpeed = 10f;
    public float runDistance = 3f; // Khoảng cách di chuyển khi đang chạy
    public float idleTime = 1f; // Thời gian đứng yên giữa các lần chạy
    public float runTime = 3f; // Thời gian chạy trước khi đứng yên

    private bool takingHit = false;
    private float hitTimer = 0f; // Biến đếm thời gian sau khi nhận sát thương

    public Animator animator; // Animator của FlyingEye
    public SpriteRenderer spriteRenderer; // SpriteRenderer của FlyingEye

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isFly = false; // Trạng thái di chuyển: true là đang chạy, false là đứng yên
    private bool movingRight = true; // Hướng di chuyển, true là sang phải, false là sang trái
    private float timer = 0f; // Biến đếm thời gian

    private bool isDead = false; // Biến kiểm tra FlyingEye đã chết hay chưa
    private Vector2 deathPosition; // Vị trí để FlyingEye dừng sau khi chết

    void Start()
    {
        movingRight = !movingRight;
        targetPosition = new Vector2(transform.position.x + (movingRight ? runDistance : -runDistance), transform.position.y);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Lấy reference đến Animator

        // Khởi tạo vị trí mục tiêu ban đầu
        targetPosition = new Vector2(transform.position.x + (movingRight ? runDistance : -runDistance), transform.position.y);
    }

    void TakeDamage()
    {
        takingHit = true;
        animator.SetTrigger("TakeHit");
        damageCount++; // Tăng biến đếm sát thương
        

    }

    void Update()
    {
        if (!isDead) // Chỉ thực hiện update nếu FlyingEye chưa chết
        {
            if (isFly && !takingHit)
            {
                MoveCharacter(targetPosition);
                spriteRenderer.flipX = !movingRight;
                // Kiểm tra nếu FlyingEye gần vị trí mục tiêu
                if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
                {
                    // Dừng di chuyển và bắt đầu tính thời gian đứng yên
                    isFly = false;
                    timer = 0f;
                    // Cập nhật parameter trong Animator
                    animator.SetBool("isFly", false);
                }
            }
            else
            {
                // Tăng thời gian đếm
                timer += Time.deltaTime;

                // Nếu đã đủ thời gian đứng yên và không đang nhận sát thương, bắt đầu chạy tiếp theo
                if (timer >= idleTime && !takingHit)
                {
                    isFly = true;
                    timer = 0f;

                    // Thay đổi hướng di chuyển
                    movingRight = !movingRight;

                    // Cập nhật vị trí mục tiêu mới
                    targetPosition = new Vector2(transform.position.x + (movingRight ? runDistance : -runDistance), transform.position.y);

                    // Cập nhật parameter trong Animator
                    animator.SetBool("isFly", true);
                }
                // TakeDamage();
            }
        
            // Xác định điều kiện để kích hoạt animation attack
            if (!takingHit && !isFly)
            {
                hitTimer += Time.deltaTime;
                
                if (hitTimer >= 1f)
                {
                    // Kích hoạt animation attack
                    animator.SetTrigger("Attack");
                    hitTimer = 0f;
                    // TakeDamage();
                }
                
            }

            if (takingHit)
            {
                // Tăng thời gian sau khi nhận sát thương
                hitTimer += Time.deltaTime;

                // Nếu đã đủ thời gian dừng sau khi nhận sát thương, tiếp tục di chuyển bình thường
                if (hitTimer >= 1f)
                {
                    takingHit = false;
                    hitTimer = 0f;
                }
            }

            // Kiểm tra điều kiện để kích hoạt animation death
            if (damageCount >= deathThreshold && !isDead)
            {
                // Kích hoạt animation death
                animator.SetTrigger("Death");
                // Reset biến đếm sát thương
                damageCount = 0;
                isDead = true; // Đã chết
                // Lưu vị trí hiện tại của FlyingEye
                deathPosition = transform.position;
            }
        }
        else // Nếu FlyingEye đã chết
        {
            // Di chuyển FlyingEye đến vị trí đã lưu (vị trí để dừng sau khi chết)
            hitTimer += Time.deltaTime;
            transform.position = deathPosition;

            // Nếu đã đủ thời gian dừng sau khi nhận sát thương, tiếp tục di chuyển bình thường
            if (hitTimer >= 2f)
            {
                OnDeathAnimationEnd();
                hitTimer = 0f;
            }
        }
    }

    // Phương thức được gọi từ Animator khi animation Death kết thúc
    public void OnDeathAnimationEnd()
    {
        // Xóa FlyingEye khỏi map
        Destroy(gameObject);
    }

    void MoveCharacter(Vector2 target)
    {
        // Di chuyển FlyingEye tới vị trí mục tiêu
        rb.MovePosition(Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime));
    }
}
