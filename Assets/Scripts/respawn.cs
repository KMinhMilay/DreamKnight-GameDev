using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawn : MonoBehaviour
{
    Vector2 startPos;
    [SerializeField] HealthBar healthBar;
    [SerializeField] ManaBar manaBar;
    Animator animator;
    bool isDead = false; // Biến để kiểm tra xem nhân vật đã chết chưa

    private void Start()
    {
        startPos = transform.position;
        HealthBar.HealthZeroEvent += RespawnOnZeroHealth;
        animator = GetComponent<Animator>(); // Lấy tham chiếu đến Animator component
    }

    private void OnDestroy()
    {
        HealthBar.HealthZeroEvent -= RespawnOnZeroHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("Fall")) // Kiểm tra nếu nhân vật chưa chết và chạm vào "Fall" collider
        {
            // Gọi hàm mất toàn bộ máu và kích hoạt trigger death
            TouchFallEvent();
        }
    }

    void TouchFallEvent()
    {
        isDead = true; // Đặt biến isDead thành true để chỉ ra rằng nhân vật đã chết
        healthBar.Damage(healthBar.maxHealth); // Gây sát thương là toàn bộ máu còn lại
        Die(); // Gọi hàm Die
        manaBar.GetMana(manaBar.maxMana);
    }

    void Die()
    {
        // Kích hoạt trigger death trong animator
        animator.SetTrigger("Death");
        // Thêm code xử lý khi nhân vật chết tại đây nếu cần
        StartCoroutine(RespawnAfterDelay(0.5f)); // Chờ 1 giây trước khi respawn
    }

    IEnumerator RespawnAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Respawn();
    }

    void Respawn()
    {
        transform.position = startPos; // Cập nhật vị trí ban đầu
        isDead = false; // Đặt lại isDead thành false khi nhân vật respawn
        if (healthBar != null)
        {
            healthBar.ResetHealth(); // Reset máu
        }
        if (manaBar != null)
        {
            manaBar.ResetMana(); // Reset mana
        }
        // Load lại màn chơi hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RespawnOnZeroHealth()
    {
        if (!isDead)
        {
            Die();
        }
    }
}
