using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDialogueStart : MonoBehaviour
{
    public DialogueTrigger trigger;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") == true)
        {
            trigger.StartDialogue();
            Destroy(this);
            
        }
        
            
            
    }
}
