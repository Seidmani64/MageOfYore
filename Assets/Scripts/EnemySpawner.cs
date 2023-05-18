using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnRange = 20;
    private Vector3 spawnPoint;
    private float x,z;
    public int numEnemies = 1;
    public bool isBoss = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {   
        if(isBoss)
        {
            SpawnBoss();
        }
        else
        {
            spawnPoint = transform.position;
            spawnPoint.x = 0;
            numEnemies = Random.Range(1,4);
            for(int i = 0; i < numEnemies; i++)
                Spawn();   
        }

    }

    public void EnemiesCheck()
    {
        if(numEnemies <= 0 && ExpManager.instance.levellingUp)
        {
            ExpManager.instance.LevelUp();
        }
        else if(numEnemies <= 0)
        {
            SceneManager.LoadScene("Overworld");
        }
            
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

     void SpawnBoss()
     {
        GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
     }
}
