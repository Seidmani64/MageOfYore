using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SettingsPrefs : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCam;
    public static SettingsPrefs instance;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume",1f);
        AudioListener.volume = volume;

        cinemachineCam = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        float horizontalSpeed = PlayerPrefs.GetFloat("HorizontalSpeed",1f);
        float verticalSpeed = PlayerPrefs.GetFloat("VerticalSpeed",1f);
        cinemachineCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = horizontalSpeed;
        cinemachineCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = verticalSpeed;
    }

    public void SetSpeed(float hSpeed, float vSpeed)
    {
        PlayerPrefs.SetFloat("HorizontalSpeed",hSpeed);
        PlayerPrefs.SetFloat("VerticalSpeed",vSpeed);
        cinemachineCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = hSpeed;
        cinemachineCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = vSpeed;
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume",volume);
        AudioListener.volume = volume;
    }

}
