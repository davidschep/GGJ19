using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class EnemyStateManager : MonoBehaviour
{
    private StateMachine stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.SwitchState(StateType.EnemyPatrollingState);
    }
}
