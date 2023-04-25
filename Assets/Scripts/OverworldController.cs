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

    private float randEncounter = 0f;


    void Start()
    {
        goal = transform.position;
    }

    void Update()
    {
        moving = (goal != transform.position);

        if(moving)
            MoveTowardsGoal();
        else
            SetNewGoal();


    }

    private void MoveTowardsGoal()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
        transform.LookAt(goal);
    }

    private void SetNewGoal()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if(hInput != 0 || vInput != 0)
            {
                randEncounter = Random.Range(0f, 1f);
                Debug.Log(randEncounter);
                if(randEncounter < 0.03f) 
                    SceneManager.LoadScene("Battle");
            }

        Vector3 tempVect = Vector3.zero;

        if(Mathf.Abs(hInput) >= Mathf.Abs(vInput))
            tempVect = new Vector3(hInput, 0, 0);
        else
            tempVect  = new Vector3(0,0,vInput);

        
        goal = transform.position + tempVect.normalized;
    }

}