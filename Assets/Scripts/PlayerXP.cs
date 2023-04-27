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

    void Update()
    {
        if(Input.GetKeyDown("t"))
        {
            exp = 0;
            PlayerPrefs.SetInt("exp", exp);
            level = 1;
            PlayerPrefs.SetInt("level", level);
            ExpManager.instance.UpdateLevel();
        }
    }

    public void AddXP(int amount)
    {
        exp += amount;
        PlayerPrefs.SetInt("exp", exp); 
        level = (int)Mathf.Floor((exp+5)/5); 
        if(level > PlayerPrefs.GetInt("level", 1))
        {
            ExpManager.instance.UpdateLevel();
            PlayerPrefs.SetInt("level", level);
        }
        
    }
}
