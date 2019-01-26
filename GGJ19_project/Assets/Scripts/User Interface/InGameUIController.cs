﻿using System.Collections;
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

    [SerializeField] private TMPro.TMP_Text foodLeftToCollectText;
    [SerializeField]  private TMPro.TMP_Text timeText;

    private void Start()
    {
        AudioController.Instance.SwitchMusicState(MusicState.CATNEARBY);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioController.Instance.PlayOneShot(SoundEffectType.PICKUP);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AudioController.Instance.SwitchMusicState(MusicState.CATCHASE);
        }
    }

    public void SetFadeInOutImageAlpha(float alpha)
    {
        fadeInOutImage.color = new Color(fadeInOutImage.color.r, fadeInOutImage.color.g, fadeInOutImage.color.b, alpha);
    }

    public void SetDayTime(int day, int time)
    {
        timeText.text = "day: " + day.ToString() + "\n" + "time left: " + time.ToString();
    }

    public void SetFoodLeftToCollectText(int amountLeft)
    {
        foodLeftToCollectText.text = amountLeft.ToString() + " food left to collect";
    }
}
