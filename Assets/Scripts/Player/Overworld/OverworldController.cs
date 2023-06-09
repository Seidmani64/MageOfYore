using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldController : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    private float hInput, vInput;
    private Vector3 move = Vector3.zero;
    private Vector3 goal = Vector3.zero;
    private bool moving = false;
    [SerializeField] private LayerMask wallLM;
    private float randEncounter = 0f;
    private DialogueManager dialogueManager;
    [SerializeField] private float distance = 1f;
    [SerializeField] private Animator animator;
    private float xInitialPos,zInitialPos;

    private int steps = 0;


    void Awake()
    {
        xInitialPos = PlayerPrefs.GetFloat("X start", 1f);
        zInitialPos = PlayerPrefs.GetFloat("Z start", 1f);
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        transform.position = new Vector3(xInitialPos, 0, zInitialPos);
        goal = transform.position;
        randEncounter = 0f;
        steps = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            PauseManager.instance.MenuCheck();
        if(Input.GetKeyDown("z"))
            if(dialogueManager.inConversation)
                dialogueManager.DisplayNextSentence();
            else
                Interact();
        if(PauseManager.instance.paused || dialogueManager.inConversation)
            return;
        moving = (goal != transform.position);

        if(moving)
            MoveTowardsGoal();
        else
            SetNewGoal();

        if(Input.GetKeyDown("t"))
        {
            PlayerPrefs.DeleteAll();
        }
        else if(Input.GetKeyDown("y"))
        {
            PlayerPrefs.SetInt("LightningWall",1);
        }

    }

    private void MoveTowardsGoal()
    {
        animator.SetBool("Walking",true);
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
        transform.LookAt(goal);
    }

    private void SetNewGoal()
    {
        animator.SetBool("Walking",false);
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if(hInput != 0 || vInput != 0)
            {
                randEncounter = Random.Range(0f, 1f);
                if(randEncounter < (0.01f + steps/200) && PlayerPrefs.GetInt("EncountersEnabled",1) >= 1) 
                {
                    randEncounter = 0;
                    steps = 0;
                    PlayerPrefs.SetFloat("X start", transform.position.x);
                    PlayerPrefs.SetFloat("Z start", transform.position.z);
                    if(PlayerPrefs.GetString("CurrentZone","Forest") == "Forest")
                        SceneManager.LoadScene("ForestBattle");
                    else if(PlayerPrefs.GetString("CurrentZone","Forest") == "Volcano")
                        SceneManager.LoadScene("LavaBattle");
                }
                steps++; 
            }

        Vector3 tempVect = Vector3.zero;

        if(Mathf.Abs(hInput) >= Mathf.Abs(vInput))
            tempVect = new Vector3(hInput, 0, 0);
        else
            tempVect  = new Vector3(0,0,vInput);

        
        goal = transform.position + tempVect.normalized * distance;
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, tempVect.normalized, out hit, distance, wallLM))
        {
            transform.LookAt(goal);
            goal = transform.position;
        }
    }

    private void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, wallLM))
        {
            DialogueTrigger interactable = hit.collider.gameObject.GetComponent<DialogueTrigger>();
            if(interactable != null)
                interactable.TriggerDialogue();
        }
    }

}
