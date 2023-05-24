using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance; 
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject resumeButton;
    private GameObject player;
    public bool paused = false;

    void Awake()
    {
        instance = this;
    }

    public void MenuCheck()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !ExpManager.instance.levellingUp)
        {
            if(paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        var eventSystem = EventSystem.current;  
        eventSystem.SetSelectedGameObject(resumeButton, new BaseEventData(eventSystem));
        Cursor.lockState = CursorLockMode.Confined;
        paused = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnMenu()
    {
        player = GameObject.FindWithTag("Player");
        PlayerPrefs.SetFloat("X start", player.transform.position.x);
        PlayerPrefs.SetFloat("Z start", player.transform.position.z);
        SceneManager.LoadScene("Menu");
    }
}
