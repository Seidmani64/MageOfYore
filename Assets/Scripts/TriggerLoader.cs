using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoader : MonoBehaviour
{
    [SerializeField] private string zone;
    private Transform player;
    private AudioSource audioSource;
    [SerializeField] private bool EnableEncounters; 
    [SerializeField] private AudioClip forestMusic;
    [SerializeField] private AudioClip volcanoMusic;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 1)
        {
            if(zone == null)
            {
                PlayerPrefs.SetInt("CurrentZone",0);
                PlayerPrefs.SetFloat("X start", 1f);
                PlayerPrefs.SetFloat("Z start", 1f);
            }
            else
            {
                AudioManager.instance.NewSong();
                if(EnableEncounters)
                    PlayerPrefs.SetInt("EncountersEnabled",1);
                else
                    PlayerPrefs.SetInt("EncountersEnabled",0);
                PlayerPrefs.SetString("CurrentZone",zone);
                PlayerPrefs.SetFloat("X start", player.position.x);
                PlayerPrefs.SetFloat("Z start", player.position.z);
            }
        }
    }

}
