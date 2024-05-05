using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Tốc độ di chuyển của goblin
    public float leftRightDistance = 9f; // Khoảng cách di chuyển sang trái và sang phải
    public float waitingTime = 1f; // Thời gian chờ trước khi di chuyển tiếp theo
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool movingRight = true; // Biến để xác định hướng di chuyển

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Xác định vị trí mục tiêu ban đầu
        targetPosition = new Vector2(transform.position.x + leftRightDistance, transform.position.y);
    }

    void Update()
    {
        // Kiểm tra nếu goblin đã đến vị trí mục tiêu
        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
        {
            // Thay đổi hướng di chuyển
            movingRight = !movingRight;
            // Xác định vị trí mục tiêu mới
            if (movingRight)
                targetPosition = new Vector2(transform.position.x + leftRightDistance, transform.position.y);
            else
                targetPosition = new Vector2(transform.position.x - leftRightDistance, transform.position.y);
            // Đặt thời gian chờ trước khi di chuyển tiếp theo
            Invoke("SetTargetPosition", waitingTime);
        }
    }

    void FixedUpdate()
    {
        // Di chuyển goblin
        MoveCharacter(targetPosition);
    }

    void MoveCharacter(Vector2 target)
    {
        // Di chuyển goblin đến vị trí mục tiêu
        rb.MovePosition(Vector2.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime));
    }

    void SetTargetPosition()
    {
        // Xác định vị trí mục tiêu tiếp theo
        if (movingRight)
            targetPosition = new Vector2(transform.position.x + leftRightDistance, transform.position.y);
        else
            targetPosition = new Vector2(transform.position.x - leftRightDistance, transform.position.y);
    }
}
