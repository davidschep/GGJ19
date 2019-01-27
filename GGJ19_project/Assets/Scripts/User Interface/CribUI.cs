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

    public void UpdateFoodCounter(int foodCounter)
    {
        foodCounterText.text = "Food: " + foodCounter + "/" + GameStateManager.MAX_FOOD_SPAWNS;
    }
}
