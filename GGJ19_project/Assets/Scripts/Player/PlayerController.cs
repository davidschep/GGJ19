using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.1f;

    private NavMeshAgent agent;
    private void Awake() 
    {
        agent = this.GetComponent<NavMeshAgent>();    
    }

    void Update()
    {
        // Vector3 lookDir = Vector3.zero;
        // Vector3 moveDir = Vector3.zero;

        // lookDir.z = moveDir.z = Input.GetAxis("Vertical");
        // lookDir.x = Input.GetAxis("Horizontal");

        // this.transform.rotation = Quaternion.LookRotation(lookDir);

        // //agent.updateRotation = true;
        // agent.Move(moveDir);

        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void Move(float horizontal, float vertical)
    {
        // create input vector, normalize in case of diagonal movement
        Vector3 input = new Vector3(horizontal * rotationSpeed, 0, vertical);


        if (input.magnitude > 1)
        {
            input = input.normalized;  
        } 

        // get camera rotation without up/down angle, only left/right
       // Vector3 angles = Camera.main.transform.rotation.eulerAngles;
        //angles.x = 0;
        //Quaternion rotation = Quaternion.Euler(angles); // back to quaternion
        Quaternion rotation = Quaternion.Euler(this.transform.eulerAngles);

        // calculate input direction relative to camera rotation
        Vector3 direction = rotation * input;

        // draw direction for debugging
        Debug.DrawLine(transform.position, transform.position + direction, Color.green, 0, false);

        // moving with velocity doesn't look at the direction, do it manually
        LookAtY(transform.position + direction);

        // set velocity
        agent.velocity = direction * agent.speed;
    }

    void LookAtY(Vector3 position)
    {
        transform.LookAt(new Vector3(position.x, this.transform.position.y, position.z));
    }
}
