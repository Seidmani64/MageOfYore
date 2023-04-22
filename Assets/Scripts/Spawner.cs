using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnRange = 10;
    [SerializeField] private float spawnTime = 5f;
    private float timer = 0f;
    private Vector3 spawnPoint;
    private float x;
    private float movementSpeed = 3.5f;
    private float increaseTimer = 5f;

    void Start()
    {   
        timer = 0f;
        spawnPoint.x = 0;
    }

    void Update()
    {
        increaseTimer -= Time.deltaTime;
        if((int)Time.timeSinceLevelLoad % 10 == 0 && increaseTimer <= 0f)
            movementSpeed += movementSpeed/3;
        timer += Time.deltaTime;
        Spawn();    
    }

    void Spawn()
     {
         if (timer >= spawnTime)
         {
             x = Random.Range(0, spawnRange);
             spawnPoint.x = x;
             GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
             enemy.GetComponent<NavMeshAgent>().speed = movementSpeed;
             timer = 0;
         }
     }
}
