using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private float startChaseDistance = 8;
    [SerializeField] private float abandonChaseDistance = 10;

    private StateMachine stateMachine;
    private bool chasing;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.SwitchState(StateType.EnemyPatrollingState);
    }

    private void Update()
    {
        if(chasing)
        {
            if(Vector3.Distance(transform.position, PlayerTest.Instance.transform.position) > abandonChaseDistance)
            {
                stateMachine.SwitchState(StateType.EnemyPatrollingState);
                chasing = false;
            }
        }
        else if(Vector3.Distance(transform.position, PlayerTest.Instance.transform.position) < startChaseDistance)
        {
            stateMachine.SwitchState(StateType.EnemyChasingState);
            chasing = true;
        }
    }
}
