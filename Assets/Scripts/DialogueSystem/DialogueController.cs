using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueController : MonoBehaviour
{
    public Text actorName;
    public Text messageText;
    public Image dialogueImg;

    public static bool isActive = false;
    Message[] currentMessages;
    Actor[] currentActors;
    int acctiveMessage = 0;
    public void Awake()
    {
        isActive = false;
        actorName.text = "";
        messageText.text = "";
        dialogueImg.enabled = isActive;
    }
    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        acctiveMessage = 0;
        isActive = true;
        dialogueImg.enabled = isActive;
        Debug.Log("Đã mở dialogue! Bắt đàu hội thoại. Load messages: " + messages.Length);
        DisplayMessage();
        
    }
    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[acctiveMessage];
        messageText.text = messageToDisplay.message;
        Actor actorToDisplay = currentActors[messageToDisplay.actorID];
        actorName.text = actorToDisplay.name;
        
    }
    private IEnumerator EndMessage()
    {
        yield return new WaitForSeconds(0.2f);
        actorName.text = "";
        messageText.text = "";
        dialogueImg.enabled = isActive;

    }
    void NextMessage()
    {
        acctiveMessage++;
        if(acctiveMessage < currentMessages.Length) 
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Kết thúc hội thoại");
            isActive = false;
            actorName.text = "";
            messageText.text = "";
            dialogueImg.enabled = isActive;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive==true)
        {
            NextMessage();
        }
    }
}
