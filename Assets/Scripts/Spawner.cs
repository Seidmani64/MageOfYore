using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnRange = 10;
    [SerializeField] private float spawnTime = 5f;
    private float timer = 0f;
    private Vector3 spawnPoint;
    private float x;

    void Start()
    {   
        timer = 0f;
        spawnPoint.x = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        Spawn();    
    }

    void Spawn()
     {
         if (timer >= spawnTime)
         {
             x = Random.Range(0, spawnRange);
             spawnPoint.x = x;
             Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
             timer = 0;
         }
     }
}
