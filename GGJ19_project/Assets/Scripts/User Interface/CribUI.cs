using System.Collections.Generic;
using UnityEngine;

public class CribUI : MonoBehaviour
{
    public static CribUI Instance { get { return GetInstance(); } }

    private static CribUI instance;

    private static CribUI GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<CribUI>();
        }
        return instance;
    }

    [SerializeField] private List<TextMesh> foodCounterTexts;
    [SerializeField] private MouseHome mouseHome;

    public void UpdateFoodCounter(int foodCounter)
    {
        for (int i = 0; i < foodCounterTexts.Count; i++)
        {
            foodCounterTexts[i].text = "Food: " + foodCounter + "/" + mouseHome.FoodNeeded;
        }
    }
}
