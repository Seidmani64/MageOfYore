using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnRange = 20;
    private Vector3 spawnPoint;
    private float x,z;
    private int enemyAmount = 1;

    void Start()
    {   
        spawnPoint = transform.position;
        spawnPoint.x = 0;
        enemyAmount = Random.Range(1,4);
        for(int i = 0; i < enemyAmount; i++)
            Spawn();   
    }

    void Update()
    {
        /*
        increaseTimer -= Time.deltaTime;
        if((int)Time.timeSinceLevelLoad % 5 == 0 && increaseTimer <= 0f)
        {
            movementSpeed += speedIncrease;
            increaseTimer = 5f;
        }
            
        timer += Time.deltaTime;
        */ 
    }

    void Spawn()
     {
        x = Random.Range(-spawnRange, spawnRange);
        if(x >= 0f && x <= 4f)
            x += 4f;
        else if(x <= 0f && x >= -4f)
            x -= 4f;
        z = Random.Range(-spawnRange, spawnRange);
        if(z >= 0f && z <= 4f)
            z += 4f;
        else if(z <= 0f && z >= -4f)
            z -= 4f;
        spawnPoint.x = x;
        spawnPoint.z = z;
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
     }
}
