using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class EnemyStateManager : MonoBehaviour
{

    [SerializeField] private float distanceToSeePlayer = 8;
    [SerializeField] [Range(-1.0f , 1.0f)] private float angleToSeePlayer = 0.7f;
    [SerializeField] private float abandonChaseTime = 5;
    [SerializeField] private LayerMask playerBlockVisibilityLayerMask;

    private StateMachine stateMachine;
    private bool chasing;
    private float abandonChaseTimer;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.SwitchState(StateType.EnemyPatrollingState);
    }

    private void Update()
    {
        bool seePlayer = CheckSeePlayer();

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
    }

    private bool CheckSeePlayer()
    {
        Vector3 directionToPlayer = (transform.position - PlayerController.Instance.transform.position).normalized;

        if (Mathf.Abs(Vector3.Dot(directionToPlayer, transform.forward.normalized)) < angleToSeePlayer) { return false; }

        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distanceToPlayer > distanceToSeePlayer) { return false; }

        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, playerBlockVisibilityLayerMask)) { return false; }

        return true;
    }
}
