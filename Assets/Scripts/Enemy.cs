using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Damage
{
    [SerializeField] private float hp = 1f;
    [SerializeField] private float maxIFrames = 1f;
    private NavMeshAgent agent;
    private Sprite originalSprite;
    private Transform player;
    private float iFrames = 0f;
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSights, playerInRange;
    [SerializeField] private LayerMask isPlayer;
    [SerializeField] private float attackSpeed;
    private bool hasAttacked;
    [SerializeField] private Animator animator;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        iFrames -= Time.deltaTime;
        if(iFrames <= 0f)
            animator.SetBool("Hurt", false);
        
        playerInSights = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if(playerInSights && !playerInRange)
            Chasing();
        else if(playerInSights && playerInRange)
            Attacking();
    }

    public virtual void Chasing()
    {
        agent.SetDestination(player.position);
    }

    public virtual void Attacking()
    {
        agent.SetDestination(transform.position);
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);
        if (!hasAttacked)
            {
                animator.SetTrigger("Attack");
                hasAttacked = true;
                Invoke(nameof(ResetAttack), attackSpeed);
            }
    }

    public void ResetAttack()
    {
        hasAttacked = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet" && iFrames <= 0)
            TakeDamage(1);
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("OOF");
        //animator.SetBool("Hurt", true);
        //iFrames = maxIFrames;
        hp -= amount;
        if(hp <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
        ScoreManager.instance.AddScore(1);    
    }
}
