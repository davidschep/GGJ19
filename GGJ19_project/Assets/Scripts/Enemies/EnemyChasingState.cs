using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChasingState : MonoBehaviour, IState
{
    public StateType StateType { get { return StateType.EnemyChasingState; } }

    [SerializeField] private float moveSpeed = 3.5f;

    private NavMeshAgent navMeshAgent;

    public virtual void Act()
    {
        navMeshAgent.SetDestination(PlayerTest.Instance.transform.position);
    }

    public virtual void Enter()
    {
        navMeshAgent.speed = moveSpeed;
    }

    public virtual void Exit() { }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
