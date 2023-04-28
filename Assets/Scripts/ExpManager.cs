using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    public static ExpManager instance; 
    public TextMeshProUGUI expUI;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject LevelUpUI;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ManaBar manaBar;
    public int exp = 0;
    public int level = 1;
    public bool levellingUp = false;
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

    public void LevelUp()
    {
        levellingUp = true;
        Cursor.lockState = CursorLockMode.Confined;
        exp = PlayerPrefs.GetInt("exp", 0);   
        level = (int)Mathf.Floor((exp+5)/5); 
        expUI.text = "Level " + level.ToString();
        Time.timeScale = 0;
        HUD.SetActive(false);
        LevelUpUI.SetActive(true);
        
    }

    public void HealthUp()
    {
        int maxHp = PlayerPrefs.GetInt("maxHp",5);
        PlayerPrefs.SetInt("maxHp", maxHp+1);
        PlayerPrefs.SetInt("hp", maxHp+1);
        HUD.SetActive(true);
        LevelUpUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        levellingUp = false;
        EnemySpawner.instance.EnemiesCheck();
    }

    public void ManaUp()
    {
        int maxMana = PlayerPrefs.GetInt("maxMana",20);
        PlayerPrefs.SetInt("maxMana", maxMana+5);
        PlayerPrefs.SetInt("mana", maxMana+5);
        HUD.SetActive(true);
        LevelUpUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        levellingUp = false;
        manaBar.SetMaxMana(maxMana);
        EnemySpawner.instance.EnemiesCheck();
    }

}
