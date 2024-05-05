using UnityEngine;

public class GoblinAnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Method to set the running state of the goblin's animation
    public void SetIsRunning(bool isRunning)
    {
        // Set the "IsRunning" parameter in the animator controller
        animator.SetBool("IsRunning", isRunning);

        // Debug log to check if the method is called and the value being passed
        Debug.Log("SetIsRunning called. IsRunning value: " + isRunning);
    }
}
