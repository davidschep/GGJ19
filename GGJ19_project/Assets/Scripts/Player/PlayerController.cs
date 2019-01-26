using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
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
    private Rigidbody rb;

    [SerializeField] private bool useVelocity = false;

    public int foodAmount = 0;
    public UnityEvent playerDeathEvent;

    private void Awake() 
    {
        agent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;

        if(playerDeathEvent == null)
            playerDeathEvent = new UnityEvent();    
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Rotate(horizontal);
        // Move(horizontal, vertical);
        PointMove(horizontal, vertical);
    }

    void PointMove(float horizontal, float vertical)
    {
        if ((horizontal != 0 || vertical != 0) || !useVelocity)
        {
            //Create input vector, normalize in case of diagonal movement.
            Vector3 input = new Vector3(horizontal, 0, vertical);
            if (input.magnitude > 1)
            {
                input = input.normalized;
            }

            //Get camera rotation without up/down angle, only left/right.
            Vector3 angles = Camera.main.transform.rotation.eulerAngles;
            angles.x = 0;
            Quaternion rotation = Quaternion.Euler(angles);

            //Calculate input direction relative to camera rotation.
            Vector3 direction = rotation * input;

            //Draw direction for debugging.
            Debug.DrawLine(transform.position, transform.position + direction, Color.green, 0, false);


            if(useVelocity)
            {
                //Moving with velocity doesn't look at the direction, do it manually.
                LookAtY(transform.position + direction);

                //Set velocity.
                agent.velocity = direction * agent.speed;
            }
            else
            {
                agent.SetDestination(this.transform.position + direction + Vector3.up);
            }
        }
    }

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

    void LookAtY(Vector3 position)
    {
        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == Tags.Enemy)
        {
            Debug.Log("Player -> Enemy Trigger Event");
            playerDeathEvent.Invoke();
        }    
    }
}
