using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get { return GetInstance(); } }

    private static PlayerController instance;

    private static PlayerController GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<PlayerController>();
        }
        return instance;
    }

    private NavMeshAgent agent;
    private void Awake() 
    {
        agent = this.GetComponent<NavMeshAgent>();    
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Rotate(horizontal);
        Move(horizontal, vertical);
    }

    // void PointMove(float horizontal, float vertical)
    // {
    //             Vector3 dir = this.transform.forward * vertical;

    //             agent.SetDestination(this.transform.position + (dir * 3));
    // }

    void Move(float horizontal, float vertical)
    {
        Vector3 input = new Vector3(horizontal, 0, vertical);

        if (input.magnitude > 1)
        {
            input = input.normalized;  
        }

        Vector3 dir = this.transform.forward * vertical;
        agent.velocity = dir * agent.speed;
        Debug.DrawLine(this.transform.position, this.transform.position + dir, Color.green, 0, false);
    }

    void Rotate(float horizontal)
    {
        transform.Rotate(Vector3.up * (Input.GetAxis("Horizontal") * agent.angularSpeed) * Time.deltaTime);
    }
}
