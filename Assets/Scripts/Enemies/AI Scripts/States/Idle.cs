using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{

    private MovementSM _sm;
    public Idle(MovementSM stateMachine) : base("Idle", stateMachine)
    {
        _sm = stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Idle State");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Transiton to "moving" state
        if (_sm.transform.position != _sm.previousPosition)
        {
            stateMachine.ChangeState(_sm.movingState);
        }

        _sm.previousPosition = _sm.transform.position;
    }
}
