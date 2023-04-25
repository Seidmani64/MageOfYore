using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleCheck : MonoBehaviour
{
    public static BattleCheck instance;
    public GameObject[] enemies;
    
    void Awake()
    {
        instance = this;
    }

    public void CheckForEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length <= 1)
            SceneManager.LoadScene("Overworld");
    }

}
