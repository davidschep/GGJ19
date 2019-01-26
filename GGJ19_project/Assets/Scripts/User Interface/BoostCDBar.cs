using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(Image))]
public class BoostCDBar : MonoBehaviour
{
    public static BoostCDBar Instance { get { return GetInstance(); } }

    private static BoostCDBar instance;

    private Slider slider;

    private static BoostCDBar GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<BoostCDBar>();
        }
        return instance;
    }

    public void SetValue(float value)
    {
        GetSlider().value = value;
    }

    private Slider GetSlider()
    {
        if(slider == null)
        {
            slider = GetComponent<Slider>();
        }
        return slider;
    }
}
