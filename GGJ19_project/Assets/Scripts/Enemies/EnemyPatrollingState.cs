using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrollingState : MonoBehaviour, IState
{
    public StateType StateType { get { return StateType.EnemyPatrollingState; } }

    private List<Point> wayPoints;
    private NavMeshAgent navMeshAgent;

    public virtual void Act()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
        {
            SetRandomWayPointDestination();
        }
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    private void Awake()
    {
        wayPoints = PointManager.Instance.GetPoints(PointType.EnemyWayPoint);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void SetRandomWayPointDestination()
    {
        print("set new waypoint");
        navMeshAgent.SetDestination(wayPoints.GetRandom().transform.position);
    }
}
