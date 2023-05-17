using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue lockedDialogue;

    public void TriggerDialogue()
    {
        Debug.Log("Enter triggerdialogue with dialogue from: " + dialogue.name);
        if(PlayerPrefs.GetInt("level",1) >= dialogue.levelRequirement)
            FindObjectOfType<DialogueManager>().StartDialogue(lockedDialogue);
        else
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }

    void LoadBattle()
    {
        SceneManager.LoadScene(dialogue.interaction.battle);
    }

}
