using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;

    public void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume",1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("HorizontalSpeed",2f);
    }
    public void SetVolume(float volume)
    {
        SettingsPrefs.instance.SetVolume(volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        SettingsPrefs.instance.SetSpeed(sensitivity, sensitivity);
    }
}
