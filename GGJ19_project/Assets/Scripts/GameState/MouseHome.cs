using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHome : MonoBehaviour
{
    public int FoodNeeded { get { return foodNeeded; } }

    [SerializeField] private int foodNeeded = 3;

    private int foodCount = 0;
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
    public int GetFood() { return foodCount;}
    public void ResetFood()
    {
        // TODO: visualize amount of food in base?
        foodCount = 0;

        InGameUIController.Instance.SetFoodCounter(foodCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            AddFood(PlayerController.Instance.foodAmount);
            PlayerController.Instance.foodAmount = 0;
        }
    }
}