using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bushPrefab;
    [SerializeField] private int spawnRange = 20;
    private Vector3 spawnPoint;
    private float x,z;
    private int bushes;

    // Start is called before the first frame update
    void Start()
    {
        bushes = Random.Range(1,4);
        spawnPoint = this.transform.position;
        for(int i = 0; i < bushes; i++)
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
            Instantiate(bushPrefab, spawnPoint, Quaternion.identity);
        }
        
    }

}
