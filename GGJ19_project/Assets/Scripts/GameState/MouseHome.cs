using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHome : MonoBehaviour
{
    [SerializeField]
    private int foodNeeded = 3;

    private int foodCount = 0;
    public void AddFood(int amount)
    {
        foodCount += amount;
        if(amount >= foodNeeded)
        {
            foodCount = 0;
            DayManager.Instance.DayCompleted();
        }

        InGameUIController.Instance.SetFoodLeftToCollectText(foodCount);
    }
    public int GetFood() { return foodCount;}
    public void ResetFood()
    {
        // TODO: visualize amount of food in base?
        foodCount = 0;

        InGameUIController.Instance.SetFoodLeftToCollectText(foodCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            // TODO: add food
            // get food of player
            // AddFood(foodamount);
            // remove food from player
        }
    }
}