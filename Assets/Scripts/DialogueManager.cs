using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    public GameObject dialogueBox;
    private bool midSentence = false;
    public bool inConversation = false;
    private bool hasInteraction;
    private string currentSentence;
    public GameObject continueButton;
    private Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue _dialogue)
    {
        dialogue = _dialogue;
        var eventSystem = EventSystem.current;  
        eventSystem.SetSelectedGameObject(continueButton, new BaseEventData(eventSystem));
        Cursor.lockState = CursorLockMode.Confined;
        inConversation = true;
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        sentences.Clear();
        hasInteraction = dialogue.hasInteraction;

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0 && !midSentence)
        {
            EndDialogue();
            return;
        }

        if(midSentence)
        {
            StopAllCoroutines();
            midSentence = false;
            dialogueText.text = currentSentence;
        }
        else
        {
            currentSentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(currentSentence));
        }


    }

    IEnumerator TypeSentence (string sentence)
    {
        midSentence = true;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.07f);
        }
        midSentence = false;
    }

    void EndDialogue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(hasInteraction)
            TriggerInteraction(dialogue.interaction);
        else
        {
            inConversation = false;
            dialogueBox.SetActive(false);
        }
        
    }

    void TriggerInteraction(Interaction interaction)
    {
        GameObject obstacleParent = interaction.obstacle.transform.root.gameObject;
        Debug.Log("Parent is: " + obstacleParent.name);
        Animator[] animators = obstacleParent.GetComponentsInChildren<Animator>();
        foreach(Animator animator in animators)
        {
            animator.SetTrigger("Activate");
        }
        if(obstacleParent.transform.Find("Obstacle") != null)
        {
            obstacleParent.transform.Find("Obstacle").gameObject.SetActive(false);
            PlayerPrefs.SetInt(interaction.obstacleName, 0);
        }
    }

}
