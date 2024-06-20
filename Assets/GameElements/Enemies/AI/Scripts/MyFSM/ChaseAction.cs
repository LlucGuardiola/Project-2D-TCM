using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Chase")]
public class ChaseAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var patrollingAgent = stateMachine.GetComponent<PatrollingAgent>();
        var enemySightSensor = stateMachine.GetComponent<EnemySightSensor>();

        patrollingAgent.SetDestination(enemySightSensor.Player.position);
    }
}