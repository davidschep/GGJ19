﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateManager : MonoBehaviour
{
    private const float CHECK_PLAYER_VISIBILITY_INTERVAL = 0.5f;

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

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        stateMachine.SwitchState(StateType.EnemyPatrollingState);
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
    }

    private bool CheckSeePlayer()
    {
        Vector3 directionToPlayer = (transform.position - PlayerController.Instance.transform.position).normalized;

        if ((Vector3.Dot(directionToPlayer, transform.forward.normalized) * -1) < angleToSeePlayer) { return false; }

        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distanceToPlayer > distanceToSeePlayer) { return false; }

        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, playerBlockVisibilityLayerMask)) { return false; }

        if(!navMeshAgent.CalculatePath(PlayerController.Instance.transform.position, navMeshPath)) { return false; }

        return true;
    }
}
