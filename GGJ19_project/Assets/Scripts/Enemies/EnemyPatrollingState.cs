using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrollingState : MonoBehaviour, IState
{
    public StateType StateType { get { return StateType.EnemyPatrollingState; } }

    [SerializeField] private float minPatrolMoveSpeed = 2.0f;
    [SerializeField] private float maxPatrolMoveSpeed = 3.5f;
    [SerializeField] private float minIdleTime = 0.1f;
    [SerializeField] private float maxIdleTime = 2.5f;
    [SerializeField] private bool randomSpeed = true;

    private List<Point> wayPoints;
    private NavMeshAgent navMeshAgent;
    private Coroutine idleCoroutine;

    public virtual void Act()
    {
        if (!navMeshAgent.hasPath && idleCoroutine == null)
        {
            idleCoroutine = StartCoroutine(Idle(() => 
            {
                navMeshAgent.speed = GetSpeed();
                SetRandomWayPointDestination();
                idleCoroutine = null;
            }));
        }
        /*
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
        {
            SetRandomWayPointDestination();
        }
        */
    }

    public virtual void Enter()
    {
        navMeshAgent.speed = GetSpeed();
    }

    public virtual void Exit()
    {
        if(idleCoroutine != null)
        {
            StopCoroutine(idleCoroutine);
        } 
    }

    private void Awake()
    {
        wayPoints = PointManager.Instance.GetPoints(PointType.EnemyWayPoint);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void SetRandomWayPointDestination()
    {
        navMeshAgent.SetDestination(wayPoints.GetRandom().transform.position);
    }

    private float GetSpeed()
    {
        if(randomSpeed)
        {
            return UnityEngine.Random.Range(minPatrolMoveSpeed, maxPatrolMoveSpeed);
        }
        else
        {
            return minPatrolMoveSpeed;
        }
    }

    private IEnumerator Test()
    {
        yield return null;
    }

    private IEnumerator Idle(Action onIdleCompletedEvent = null)
    {
        navMeshAgent.speed = minPatrolMoveSpeed;
        yield return new WaitForSeconds(UnityEngine.Random.Range(minIdleTime, maxIdleTime));

        if(onIdleCompletedEvent != null)
        {
            onIdleCompletedEvent();
        }
    }
}
