using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue lockedDialogue;
    public int levelRequirement = 1;

    public void TriggerDialogue()
    {
        if(PlayerPrefs.GetInt("level",1) >= levelRequirement)
            FindObjectOfType<DialogueManager>().StartDialogue(lockedDialogue);
        else
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }

}
