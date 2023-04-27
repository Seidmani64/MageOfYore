using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    public static ExpManager instance; 
    public TextMeshProUGUI expUI;
    public int exp = 0;
    public int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        exp = PlayerPrefs.GetInt("exp", 0);   
        level = (int)Mathf.Floor((exp+5)/5); 
        expUI.text = "Level " + level.ToString();
    }

    void Awake()
    {
        instance = this;
    }

    public void UpdateLevel()
    {
        exp = PlayerPrefs.GetInt("exp", 0);   
        level = (int)Mathf.Floor((exp+5)/5); 
        expUI.text = "Level " + level.ToString();
    }

}
