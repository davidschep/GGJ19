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

    [SerializeField]
    private Image fadeInOutImage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFadeInOutImageAlpha(float alpha)
    {
        fadeInOutImage.color = new Color(fadeInOutImage.color.r, fadeInOutImage.color.g, fadeInOutImage.color.b, alpha);
    }
}
