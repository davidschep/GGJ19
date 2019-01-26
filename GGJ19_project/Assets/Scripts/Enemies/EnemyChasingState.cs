using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChasingState : MonoBehaviour, IState
{
    public StateType StateType { get { return StateType.EnemyChasingState; } }

    private NavMeshAgent navMeshAgent;

    public virtual void Act()
    {
        navMeshAgent.SetDestination(PlayerTest.Instance.transform.position);
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
