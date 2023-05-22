using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SlimeBoss : Enemy
{

    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePos;
    [SerializeField] private int numShots;
    private bool charging;
    [SerializeField] private int numBounces = 1;
    private int bounces;
    private float previousSpeed;
    [SerializeField] private LayerMask walls;
    private Vector3 nextObjective = Vector3.zero;
    private bool bouncing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        previousSpeed = agent.speed;
        bounces = numBounces + 1;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Charging state is: " + charging);
        if(charging)
            return;
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
            int attackNum = Random.Range(0,10);
            attackNum = 8;
            if(attackNum < 8)
                animator.SetTrigger("Attack1");
            else
                animator.SetTrigger("Attack2");
            hasAttacked = true;
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
        Invoke(nameof(ResetAttack), attackSpeed);
    }

    public void Charge()
    {
        charging = true;
        RaycastHit rayHit;
        Vector3 originPoint = transform.position + new Vector3(0,3,0);
        Physics.Raycast(originPoint, player.transform.position, out rayHit, Mathf.Infinity, walls);
        Vector3 objective = rayHit.point;
        objective.y = transform.position.y;
        agent.SetDestination(objective);    
        agent.speed = 26;
        Vector3 incomingVec = rayHit.point - transform.position;
        Vector3 nextTrajectory = Vector3.Reflect(incomingVec, rayHit.normal);
        nextObjective = nextTrajectory - rayHit.point;
        nextObjective.y = transform.position.y;
    }

    public override void Die()
    {
        Destroy(gameObject);
        PlayerPrefs.SetInt("LightningWall",0);
        PlayerXP.instance.AddXP(5);
        EnemySpawner.instance.numEnemies--;
        EnemySpawner.instance.EnemiesCheck(); 
    }

    public void Bounce()
    {
        bounces--;
        if(bounces <= 0)
        {
            charging = false;
            bouncing = false;
            bounces = numBounces;
            agent.SetDestination(player.transform.position);
            agent.speed = previousSpeed;
            nextObjective = Vector3.zero;
            Invoke(nameof(ResetAttack), attackSpeed);
            return;
        }
        RaycastHit newRayHit;
        Vector3 originPoint = transform.position + new Vector3(0,3,0);
        Physics.Raycast(originPoint, nextObjective, out newRayHit, Mathf.Infinity, walls);
        Vector3 objective = newRayHit.point;
        objective.y = transform.position.y;
        agent.SetDestination(objective);    
        Vector3 incomingVec = newRayHit.point - transform.position;
        Vector3 nextTrajectory = Vector3.Reflect(incomingVec, newRayHit.normal);
        nextTrajectory.y = transform.position.y;
        nextObjective = nextTrajectory - newRayHit.point;
        nextObjective.y = transform.position.y;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet")
            TakeDamage(1);
        if(charging && col.gameObject.tag == "Wall" && !bouncing)
        {
            bouncing = true;
            Bounce();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Wall" && bouncing)
        {
            bouncing = false;
        }
    }

}

