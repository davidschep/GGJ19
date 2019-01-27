using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public static InGameUIController Instance { get { return GetInstance(); } }
    private static InGameUIController instance;
    private static InGameUIController GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<InGameUIController>();
        }
        return instance;
    }

    [SerializeField] private Image fadeInOutImage;

    [SerializeField] private TMPro.TMP_Text timeText;
    [SerializeField] private Slider playerBoostCdSlider;

    public void SetFadeInOutImageAlpha(float alpha)
    {
        fadeInOutImage.color = new Color(fadeInOutImage.color.r, fadeInOutImage.color.g, fadeInOutImage.color.b, alpha);
    }

    public void SetDayTime(int day, int time)
    {
        timeText.text = "day: " + day.ToString() + "\n" + "time left: " + time.ToString();
    }

    public void SetFoodCounter(int amountLeft)
    {
        CribUI.Instance.UpdateFoodCounter(amountLeft);
    }

    public void SetPlayerBoostCDValue(float value)
    {
        playerBoostCdSlider.value = value;
    }
}
