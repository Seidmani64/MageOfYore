using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue lockedDialogue;

    public void TriggerDialogue()
    {
        if(PlayerPrefs.GetInt("level",1) >= dialogue.levelRequirement)
            FindObjectOfType<DialogueManager>().StartDialogue(lockedDialogue);
        else
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }

}
