using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip forestMusic;
    [SerializeField] private AudioClip volcanoMusic;
    [SerializeField] private float forestVolume;
    [SerializeField] private float volcanoVolume;
    private AudioSource audioSource;
    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        string zone = PlayerPrefs.GetString("CurrentZone","Forest");
        if(zone == "Forest")
        {
            PlaySong(forestMusic,forestVolume);
        }
        else if(zone == "Volcano")
        {
            PlaySong(volcanoMusic,volcanoVolume);
        }
    }

    public void NewSong()
    {
        string zone = PlayerPrefs.GetString("CurrentZone","Forest");
        if(zone == "Forest")
        {
            if(audioSource.clip != forestMusic)
                PlaySong(forestMusic,forestVolume);
        }
        else if(zone == "Volcano")
        {
            if(audioSource.clip != volcanoMusic)
                PlaySong(volcanoMusic,volcanoVolume);
        }
    }

    public void PlaySong(AudioClip song, float volume)
    {
        audioSource.clip = song;
        audioSource.volume = volume;
        audioSource.Play();
    }


}
