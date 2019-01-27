using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickup : MonoBehaviour
{
    // Pickup could become a base class for different types of pickups, if we would add this

    [SerializeField] private float rotateSpeed = 360; // degrees in seconds
    [SerializeField] private float moveUpAmount = 0.4f;
    [SerializeField] private float moveUpSpeed = 0.4f;

    private enum MovingState
    {
        UP = 0,
        DOWN
    }
    private MovingState movingState = MovingState.UP;
    private float originalYPos;

    private void Start()
    {
        originalYPos = transform.position.y;
    }

    private void Update()
    {
        // moving up and down
        if(movingState == MovingState.UP)
        {
            transform.position += new Vector3(0f, moveUpSpeed * Time.deltaTime, 0f);
            if(transform.position.y > originalYPos + moveUpAmount)
            {
                movingState = MovingState.DOWN;
            }
        }
        else
        {
            transform.position -= new Vector3(0f, moveUpSpeed * Time.deltaTime, 0f);
            if (transform.position.y < originalYPos)
            {
                movingState = MovingState.UP;
            }
        }

        // rotating
        transform.localEulerAngles += new Vector3(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            PlayerController.Instance.SetFoodAmount(PlayerController.Instance.GetFoodAmount() + 1);
            gameObject.SetActive(false);
        }
    }
}
