using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RedBones : Enemy
{
    [SerializeField] private float range = 5f;
    private Vector3 evadePos = Vector3.zero;
    private bool evading = false;
    [SerializeField] private float evadeTime = 3f;
    private float elapsedEvadeTime = 0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        recoveryTime += Time.deltaTime;
        playerInSights = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if(evading)
        {
            if(Vector3.Distance(transform.position, evadePos) < 3)
                Evade();
            elapsedEvadeTime += Time.deltaTime;
            if(elapsedEvadeTime >= evadeTime)
            {
                evading = false;
                elapsedEvadeTime = 0f;
            }  
        }
        else if(playerInSights && !playerInRange)
            Chasing();
        else if(playerInSights && playerInRange)
            Attacking();
    }

    void Evade()
    {
        evading = true;
        evadePos = new Vector3(transform.position.x + Random.Range(-range,range),transform.position.y,transform.position.z + Random.Range(-range,range));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(evadePos, out hit, Mathf.Infinity, NavMesh.AllAreas))
            evadePos = hit.position;
        else
            Evade();
        agent.SetDestination(evadePos);
    }

    public override void TakeDamage(int amount)
    {
        Evade();
        flash.Flash();
        recoveryTime = 0f;
        hp -= amount;
        if(hp <= 0)
            Die();
    }


}

