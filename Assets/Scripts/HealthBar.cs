using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void SetHealth(int amount)
    {
        slider.value = amount;
    }

    public void SetMaxHealth(int amount)
    {
        slider.maxValue = amount;
        slider.value = amount;
    }
    
}
