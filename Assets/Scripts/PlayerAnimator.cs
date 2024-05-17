using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
    //References
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;

    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    public IEnumerator WaitForAnimationToEnd(string currentAnimation, string nextAnimation)
    {
        // Lặp lại cho đến khi animation không còn playing nữa
        while (am.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation))
        {
            yield return null;
        }

        // Khi animation kết thúc, chuyển animation về animation khác
        pm.ChangeAnimationState(nextAnimation);
    }

    void Update()
    {
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker()
    {
        if (pm.lastHorizontalVector < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
    
}
