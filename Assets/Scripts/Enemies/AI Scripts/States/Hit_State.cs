using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_State : BaseState
{
    private BehaviourSM _sm;
    public Hit_State(BehaviourSM stateMachine) : base("Hit", stateMachine)
    {
        _sm = stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
       
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // Transiton to "moving" state


        if (_sm.currentDistance > _sm.closestDistance)
        {

            stateMachine.ChangeState(_sm.movingState);

        }
        if (_sm.currentDistance <= _sm.closestDistance)
        {

            stateMachine.ChangeState(_sm.AttackState);


        }

    }
}
