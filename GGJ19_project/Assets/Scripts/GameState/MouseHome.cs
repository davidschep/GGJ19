using UnityEngine;

public class MouseHome : MonoBehaviour
{
    public int FoodNeeded { get { return foodNeeded; } }
    public int GetFood() { return foodCount;}

    [SerializeField] private int foodNeeded = 3;
    [SerializeField] private Camera mouseHomeCamera;
    [SerializeField] private Transform insideSpawnPoint;
    [SerializeField] private Transform outsideSpawnPoint;

    private int foodCount = 0;
    private bool playerInside = false;

    public void AddFood(int amount)
    {
        foodCount += amount;
        if(foodCount >= foodNeeded)
        {
            foodCount = 0;
            DayManager.Instance.DayCompleted();
        }

        InGameUIController.Instance.SetFoodCounter(foodCount);
    }

    public void ResetFood()
    {
        // TODO: visualize amount of food in base?
        foodCount = 0;

        InGameUIController.Instance.SetFoodCounter(foodCount);
    }

    private void OnPlayerHitEntrance()
    {
        if(playerInside)
        {
            PlayerController.Instance.transform.position = outsideSpawnPoint.position;
            PlayerController.Instance.SetCameraActivate(true);
            mouseHomeCamera.gameObject.SetActive(false);
        }
        else
        {
            PlayerController.Instance.SetCameraActivate(false);
            mouseHomeCamera.gameObject.SetActive(true);
            PlayerController.Instance.transform.position = insideSpawnPoint.position;
            AddFood(PlayerController.Instance.GetFoodAmount());
            PlayerController.Instance.SetFoodAmount(0);
        }

        playerInside = !playerInside;
    }

    private void Awake()
    {
        mouseHomeCamera.gameObject.SetActive(playerInside);
        PlayerController.Instance.SetCameraActivate(!playerInside);
    }

    private void OnEnable()
    {
        MouseHouseEntrance.PlayerHitEntranceEvent += OnPlayerHitEntrance;
    }

    private void OnDisable()
    {
        MouseHouseEntrance.PlayerHitEntranceEvent -= OnPlayerHitEntrance;
    }
}