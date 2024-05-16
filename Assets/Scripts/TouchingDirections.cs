using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
   
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private float groundDistance = 0.05f;
    [SerializeField]
    private bool _isGrounded;


     // References
    PlayerMovement playerMovement;
    Animator animator;

    public bool IsGrounded {
        get { 
            return _isGrounded;
        }
        private set { 
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
            playerMovement.canJump = value;
        }
    }

    void Start()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
