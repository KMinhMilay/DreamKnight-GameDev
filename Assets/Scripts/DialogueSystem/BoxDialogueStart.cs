using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDialogueStart : MonoBehaviour
{
    public DialogueTrigger trigger;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")==true)
        trigger.StartDialogue();
    }

}
