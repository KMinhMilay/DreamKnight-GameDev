using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashBehavior : MonoBehaviour
{
    private float speed = 80f;
    private Vector2 startingPosition;
    float distanceTravelled;
    float maxDistanceAllowed = 20f;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    void Update()
    {
        distanceTravelled = Vector2.Distance(startingPosition, transform.position);
        if (distanceTravelled >= maxDistanceAllowed)
        {
            Destroy(gameObject);
        }
        //transform.position += transform.right * Time.deltaTime * speed;
        rb.velocity = new Vector2(transform.localScale.x * speed, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if ()
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(gameObject);
    }
}
