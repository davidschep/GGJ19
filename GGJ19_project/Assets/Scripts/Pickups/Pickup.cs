﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Pickup could become a base class for different types of pickups, if we would add this

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            PlayerController.Instance.foodAmount++;
            gameObject.SetActive(false);
        }
    }
}