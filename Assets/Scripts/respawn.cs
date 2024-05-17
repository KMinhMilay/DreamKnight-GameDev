using System.Collections;
using UnityEngine;

public class respawn : MonoBehaviour
{
    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
        HealthBar.HealthZeroEvent += RespawnOnZeroHealth;
    }

    private void OnDestroy()
    {
        HealthBar.HealthZeroEvent -= RespawnOnZeroHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fall"))
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(RespawnAfterDelay(0.5f));
    }

    IEnumerator RespawnAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Respawn();
    }

    void Respawn()
    {
        transform.position = startPos; // Cập nhật vị trí ban đầu
        HealthBar healthBar = GetComponent<HealthBar>();
        if (healthBar != null)
        {
            healthBar.ResetHealth();
        }
    }

    void RespawnOnZeroHealth()
    {
        StartCoroutine(RespawnAfterDelay(0.5f));
    }
}
