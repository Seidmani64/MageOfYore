using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnRange = 20;
    [SerializeField] private float spawnTime = 5f;
    private float timer = 0f;
    private Vector3 spawnPoint;
    private float x,z;
    private float movementSpeed = 3.5f;
    private float speedIncrease = 3.5f*0.25f;
    private float increaseTimer = 5f;

    void Start()
    {   
        timer = 0f;
        spawnPoint = transform.position;
        spawnPoint.x = 0;
    }

    void Update()
    {
        increaseTimer -= Time.deltaTime;
        if((int)Time.timeSinceLevelLoad % 5 == 0 && increaseTimer <= 0f)
        {
            movementSpeed += speedIncrease;
            increaseTimer = 5f;
        }
            
        timer += Time.deltaTime;
        Spawn();    
    }

    void Spawn()
     {
         if (timer >= spawnTime)
         {
             x = Random.Range(-spawnRange, spawnRange);
             z = Random.Range(-spawnRange, spawnRange);
             spawnPoint.x = x;
             spawnPoint.z = z;
             GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
             enemy.GetComponent<Enemy>().SetSpeed(movementSpeed);
             timer = 0;
         }
     }
}
