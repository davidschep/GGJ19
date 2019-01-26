using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isOffLinking = false;

    private void Awake() 
    {
        agent = this.GetComponent<NavMeshAgent>();    
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // if(agent.isOnOffMeshLink && !isOffLinking)
        // {
        //     StartCoroutine(MoveAcrossNavMeshLink());
        //     isOffLinking = true;
        // }

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
        // Vector3 input = new Vector3(horizontal * rotationSpeed, 0, vertical);

        // if (input.magnitude > 1)
        // {
        //     input = input.normalized;  
        // } 

        // Quaternion rotation = Quaternion.Euler(this.transform.eulerAngles);
        // Vector3 direction = rotation * input;

        //LookAtY(transform.position + direction);
        //agent.velocity = direction * agent.speed;

        Vector3 dir = this.transform.forward * vertical;
        agent.velocity = dir * agent.speed;
        Debug.DrawLine(this.transform.position, this.transform.position + dir, Color.green, 0, false);
    }

    void Rotate(float horizontal)
    {
        transform.Rotate(Vector3.up * (Input.GetAxis("Horizontal") * agent.angularSpeed) * Time.deltaTime);
    }

    // void LookAtY(Vector3 position)
    // {
    //     transform.LookAt(new Vector3(position.x, this.transform.position.y, position.z));
    // }
 
    // IEnumerator MoveAcrossNavMeshLink()
    // {
    //     OffMeshLinkData data = agent.currentOffMeshLinkData;
    //     agent.updateRotation = false;

    //     Vector3 startPos = agent.transform.position;
    //     Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
    //     float duration = (endPos-startPos).magnitude / agent.velocity.magnitude;
    //     float t = 0.0f;
    //     float tStep = 1.0f/duration;
    //     while(t<1.0f)
    //     {
    //         transform.position = Vector3.Lerp(startPos,endPos,t);
    //         agent.destination = transform.position;
    //         t+=tStep*Time.deltaTime;
    //         yield return null;
    //     }
    //     transform.position = endPos;
    //     agent.updateRotation = true;
    //     agent.CompleteOffMeshLink();
    //     isOffLinking= false; 
    // }
}
