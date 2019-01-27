using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateManager : MonoBehaviour
{
    public static EnemyStateManager Instance { get { return GetInstance(); } }
    private static EnemyStateManager instance;
    private static EnemyStateManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<EnemyStateManager>();
        }
        return instance;
    }

    private const float CHECK_PLAYER_VISIBILITY_INTERVAL = 0.5f;

    [SerializeField] private float autoChaseDistance = 5;
    [SerializeField] private float distanceToSeePlayer = 8;
    [SerializeField] [Range(-1.0f , 1.0f)] private float angleToSeePlayer = 0.7f;
    [SerializeField] private float abandonChaseTime = 5;
    [SerializeField] private LayerMask playerBlockVisibilityLayerMask;

    private StateMachine stateMachine;
    private NavMeshAgent navMeshAgent;
    private bool chasing;
    private float abandonChaseTimer;
    private float checkPlayerVisibilityTimer;
    private bool seePlayer;
    private NavMeshPath navMeshPath;
    private Animator animator;
    private EnemyChasingState enemyChasingState;

    private void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        enemyChasingState = this.GetComponent<EnemyChasingState>();
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        stateMachine.SwitchState(StateType.EnemyPatrollingState);
        PlayerController.Instance.playerDeathEvent.AddListener(KillPlayer);
    }

    private void Update()
    {
        checkPlayerVisibilityTimer -= Time.deltaTime;
        if(checkPlayerVisibilityTimer <= 0)
        {
            seePlayer = CheckSeePlayer();
            checkPlayerVisibilityTimer = CHECK_PLAYER_VISIBILITY_INTERVAL;
        }

        if (seePlayer)
        {
            abandonChaseTimer = abandonChaseTime;

            if (!chasing)
            {
                stateMachine.SwitchState(StateType.EnemyChasingState);
                chasing = true;
            }
        }
        else if(chasing)
        {
            abandonChaseTimer -= Time.deltaTime;

            if (abandonChaseTimer <= 0)
            {
                stateMachine.SwitchState(StateType.EnemyPatrollingState);
                chasing = false;
            }
        }

        //Debug.Log(enemyChasingState.moveSpeed + " " + navMeshAgent.velocity.magnitude);
        animator.SetFloat("Speed", Mathf.Clamp01(navMeshAgent.velocity.magnitude / enemyChasingState.moveSpeed));
    }

    private bool CheckSeePlayer()
    {
        Vector3 directionToPlayer = (PlayerController.Instance.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, playerBlockVisibilityLayerMask)) { return false; }
        if (!navMeshAgent.CalculatePath(PlayerController.Instance.transform.position, navMeshPath)) { return false; }
        if (distanceToPlayer <= autoChaseDistance) { return true; }
        if (distanceToPlayer > distanceToSeePlayer) { return false; }
        if (Vector3.Dot(directionToPlayer, transform.forward.normalized) < angleToSeePlayer) { return false; }

        return true;
    }

    private void KillPlayer()
    {
        Debug.Log("Player Killed!");
    }
}
