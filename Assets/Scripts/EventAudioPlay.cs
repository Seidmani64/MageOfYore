using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAudioPlay : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void PlaySound()
    {
        audioSource.Play();
    }
}
