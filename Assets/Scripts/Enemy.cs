using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Damage
{
    [SerializeField] private float hp = 1f;
    private NavMeshAgent agent;
    private Sprite originalSprite;
    private Transform player;
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSights, playerInRange;
    [SerializeField] private LayerMask isPlayer;
    [SerializeField] private float attackSpeed;
    private bool hasAttacked;
    [SerializeField] private Animator animator;
    private float recoveryTime = 0f;
    [SerializeField] private float maxRecoveryTime = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        recoveryTime += Time.deltaTime;
        if(recoveryTime >= maxRecoveryTime)
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
        if(col.gameObject.tag == "Bullet")
            TakeDamage(1);
    }

    public void TakeDamage(int amount)
    {
        animator.SetBool("Hurt", true);
        recoveryTime = 0f;
        hp -= amount;
        if(hp <= 0)
            Die();
    }

    public void SetSpeed(float amount)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = amount;
    }

    public void Die()
    {
        Destroy(gameObject);
        BattleCheck.instance.CheckForEnemies();
        ScoreManager.instance.AddScore(1);    
    }
}
