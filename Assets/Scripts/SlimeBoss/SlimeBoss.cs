using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SlimeBoss : Enemy
{

    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePos;
    [SerializeField] private int numShots;

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

    public override void Attacking()
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

    public void Shoot()
    {
        agent.SetDestination(transform.position);
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);
        for(float i = -numShots/2; i <= numShots/2; i++)
        {
            Vector3 direction = (targetPosition - transform.position);
            direction = Quaternion.Euler(0, i * 140/numShots, 0) * direction;
            GameObject thunderBullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
            thunderBullet.GetComponent<ThunderBullet>().LaunchBullet(direction);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
        PlayerXP.instance.AddXP(5);
        EnemySpawner.instance.numEnemies--;
        EnemySpawner.instance.EnemiesCheck(); 
    }
}

