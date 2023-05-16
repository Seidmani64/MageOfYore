using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionTrigger : MonoBehaviour
{
    public Interaction interaction;

    void TriggerEvent(Interaction interaction)
    {
        interaction.obstacle.SetActive(false);
        PlayerPrefs.SetInt(interaction.obstacleName, 0);
        if(interaction.battle != null)
            SceneManager.LoadScene(interaction.battle);
    }

}
