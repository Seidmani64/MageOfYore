using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highscoreUI;
    public int score = 0;
    public int highscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);    
        scoreUI.text = "Score: 0";
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score++;
        string scoreMsg = "Score: " + score.ToString();
        scoreUI.text = scoreMsg;
        if(highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }
}
