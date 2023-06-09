using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, Damage
{
    [SerializeField] public float hp = 1f;
    public NavMeshAgent agent;
    public Transform player;
    [SerializeField] public float sightRange, attackRange;
    [SerializeField] public bool playerInSights, playerInRange;
    [SerializeField] public LayerMask isPlayer;
    [SerializeField] public float attackSpeed;
    public bool hasAttacked;
    [SerializeField] public Animator animator;
    public float recoveryTime = 0f;
    [SerializeField] public float maxRecoveryTime = 0.5f;
    public static int numEnemies = 0;
    [SerializeField] public FlashEffect flash;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        numEnemies++;
    }

    // Update is called once per frame
    void Update()
    {
        recoveryTime += Time.deltaTime;
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

    public virtual void TakeDamage(int amount)
    {
        flash.Flash();
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

    public virtual void Die()
    {
        Destroy(gameObject);
        PlayerXP.instance.AddXP(1);
        EnemySpawner.instance.numEnemies--;
        EnemySpawner.instance.EnemiesCheck(); 
    }
}