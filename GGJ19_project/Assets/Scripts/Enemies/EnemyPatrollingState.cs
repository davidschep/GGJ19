using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : MonoBehaviour, IState
{
    public StateType StateType { get { return StateType.EnemyPatrollingState; } }

    private List<Point> wayPoints;

    public virtual void Act() { }

    public virtual void Enter() { }

    public virtual void Exit() { }

    private void Awake()
    {
        wayPoints = PointManager.Instance.GetPoints(PointType.EnemyWayPoint);
    }
}
