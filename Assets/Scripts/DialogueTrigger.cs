using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool hasLockedDialogue = false;
    public Dialogue lockedDialogue;

    public void TriggerDialogue()
    {
        if(PlayerPrefs.GetInt("level",1) >= lockedDialogue.levelRequirement && hasLockedDialogue)
            FindObjectOfType<DialogueManager>().StartDialogue(lockedDialogue);
        else
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }

    public void EndInteraction()
    {
        FindObjectOfType<DialogueManager>().inConversation = false;
    }

}
