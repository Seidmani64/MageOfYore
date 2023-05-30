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
    private float maxHp;
    private bool immune = false;
    private bool ultimateAvailable = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        previousSpeed = agent.speed;
        bounces = numBounces + 1;
        player = GameObject.FindWithTag("Player").transform;
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= maxHp/2 && ultimateAvailable)
        {
            animator.SetTrigger("Attack2");
            hasAttacked = true;
        }
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
            animator.SetTrigger("Attack1");
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
        ultimateAvailable = false;
        charging = true;
        immune = true;
        RaycastHit rayHit;
        Vector3 originPoint = transform.position;
        originPoint.y = 0;
        Vector3 direction = new Vector3(Random.Range(0,2)*2-1,0,Random.Range(0,2)*2-1);
        Physics.Raycast(originPoint, direction.normalized, out rayHit, Mathf.Infinity, walls);
        Vector3 objective = rayHit.point;
        agent.SetDestination(objective);    
        agent.speed = 20;
        agent.angularSpeed = 200;
        agent.acceleration = 150;
        Vector3 incomingVec = rayHit.point - originPoint;
        Vector3 nextTrajectory = Vector3.Reflect(incomingVec, rayHit.normal);
        nextObjective = nextTrajectory - rayHit.point;
        nextObjective.y = transform.position.y;
    }

    public override void Die()
    {
        Destroy(gameObject);
        PlayerPrefs.SetInt("LightningWall",0);
        PlayerPrefs.SetInt("EncountersEnabled", 0);
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
            immune = false;
            bounces = numBounces;
            agent.SetDestination(player.transform.position);
            agent.speed = previousSpeed;
            agent.angularSpeed = 120;
            agent.acceleration = 60;
            nextObjective = Vector3.zero;
            Invoke(nameof(ResetAttack), attackSpeed);
            return;
        }
        agent.speed += 5;
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
        if(col.gameObject.tag == "Bullet" && !immune)
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

