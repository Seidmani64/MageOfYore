using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMana(int amount)
    {
        slider.value = amount;
    }

    public void SetMaxMana(int amount)
    {
        slider.maxValue = amount;
        slider.value = amount;
    }
}
