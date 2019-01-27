using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // singleton
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

    public Camera PlayerCamera { get { return playerCamera; } }
    private NavMeshAgent agent;
    private Rigidbody rb;

    [SerializeField] private bool useVelocity = false;
    [SerializeField] private float boostTime = 0.75f;
    [SerializeField] private float boostCooldown = 5;
    [SerializeField] private float normalSpeed = 15;
    [SerializeField] private float boostSpeed = 25;
    [SerializeField] private Camera playerCamera;

    private float baseAcceleration;

    const float slowDownPerFood = 2f;
    private float GetNormalSpeed() { return normalSpeed - slowDownPerFood * (float)foodAmount; }
    private float GetBoostSpeed() { return boostSpeed - slowDownPerFood * (float)foodAmount; }

    public UnityEvent playerDeathEvent;

    private Coroutine boostCoroutine;
    private float boostCooldownTimer;

    // food
    [SerializeField] private Transform backpackTransform;
    private int foodAmount = 0;
    public int GetFoodAmount() { return foodAmount; }
    public void SetFoodAmount(int food)
    {
        foodAmount = food;
        if (foodAmount > 2)
        {
            backpackTransform.localScale = new Vector3(3.2f, 3.2f, 3.2f);
        }
        else if (foodAmount > 1)
        {
            backpackTransform.localScale = new Vector3(2.6f, 2.6f, 2.6f);
        }
        else if (foodAmount > 0)
        {
            backpackTransform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        else
        {
            backpackTransform.localScale = Vector3.one;
        }
        agent.speed = GetNormalSpeed();
        agent.acceleration = baseAcceleration;
    }

    public void SetCameraActivate(bool enable)
    {
        playerCamera.gameObject.SetActive(enable);
    }

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;
        agent.speed = GetNormalSpeed();
        baseAcceleration = agent.acceleration;

        if (playerDeathEvent == null)
            playerDeathEvent = new UnityEvent();
    }

    void Update()
    {
        boostCooldownTimer -= Time.deltaTime;

        InGameUIController.Instance.SetPlayerBoostCDValue(Mathf.Abs(Mathf.Clamp01(boostCooldownTimer / boostCooldown) - 1));

        if (boostCoroutine == null && boostCooldownTimer < 0 && (Input.GetKey(KeyCode.LeftShift) || (Input.GetAxis("Fire1") > 0.8f || Input.GetAxis("Fire2") > 0.8f)))
        {
            boostCooldownTimer = boostCooldown;
            boostCoroutine = StartCoroutine(Boost());
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Rotate(horizontal);
        // Move(horizontal, vertical);
        PointMove(horizontal, vertical);
    }

    private IEnumerator Boost()
    {
        float previousAcceleration = agent.acceleration;
        agent.acceleration = 100000;
        agent.speed = GetBoostSpeed();
        yield return new WaitForSeconds(boostTime);
        agent.speed = GetNormalSpeed();
        agent.acceleration = previousAcceleration;
        boostCoroutine = null;
    }

    void PointMove(float horizontal, float vertical)
    {
        if (playerCamera.enabled && ((horizontal != 0 || vertical != 0) || !useVelocity))
        {
            //Create input vector, normalize in case of diagonal movement.
            Vector3 input = new Vector3(horizontal, 0, vertical);
            if (input.magnitude > 1)
            {
                input = input.normalized;
            }

            //Get camera rotation without up/down angle, only left/right.
            Vector3 angles = playerCamera.transform.rotation.eulerAngles;
            angles.x = 0;
            Quaternion rotation = Quaternion.Euler(angles);

            //Calculate input direction relative to camera rotation.
            Vector3 direction = rotation * input;

            //Draw direction for debugging.
            Debug.DrawLine(transform.position, transform.position + direction, Color.green, 0, false);


            if (useVelocity)
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
        if (other.tag == Tags.Enemy)
        {
            Debug.Log("Player -> Enemy Trigger Event");
            playerDeathEvent.Invoke();
        }
    }
}
