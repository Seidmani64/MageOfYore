using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public static PlayerXP instance;
    private int exp;
    private int level;

    void Start()
    {
        exp = PlayerPrefs.GetInt("exp", 0);
        level = PlayerPrefs.GetInt("level", 1);
    }

    void Awake()
    {
        instance = this;
    }

    public void AddXP(int amount)
    {
        exp += amount;
        PlayerPrefs.SetInt("exp", exp); 
        level = (int)Mathf.Floor((exp+5)/5); 
        if(level > PlayerPrefs.GetInt("level", 1))
        {
            ExpManager.instance.UpdateLevel();
            Debug.Log("Level Up!");
            PlayerPrefs.SetInt("level", level);
        }
        
    }
}
