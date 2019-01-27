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

    [SerializeField] private TextMesh foodCounterText;
    [SerializeField] private MouseHome mouseHome;

    public void UpdateFoodCounter(int foodCounter)
    {
        foodCounterText.text = "Food: " + foodCounter + "/" + mouseHome.FoodNeeded;
    }
}
