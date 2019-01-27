using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHome : MonoBehaviour
{
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

        InGameUIController.Instance.SetFoodLeftToCollectText(foodNeeded - foodCount);
    }
    public int GetFood() { return foodCount;}
    public void ResetFood()
    {
        // TODO: visualize amount of food in base?
        foodCount = 0;

        InGameUIController.Instance.SetFoodLeftToCollectText(foodNeeded);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddFood(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            AddFood(PlayerController.Instance.GetFoodAmount());
            PlayerController.Instance.SetFoodAmount(0);
        }
    }
}