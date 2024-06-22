using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Attack_State : BaseState
{

    private BehaviourSM _sm;
    public Attack_State(BehaviourSM stateMachine) : base("Attack", stateMachine) 
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
        _sm.currentDistance = Vector3.Distance(_sm.player.transform.position, _sm.transform.position);
        if (_sm.currentDistance <= _sm.closestDistance)
        {
            _sm.attackScript.TryStartAttack(2,_sm.attack_Anim,_sm.animator,1.2f);
        }

    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Debug.Log(_sm.attackScript.isAttacking);
        if (!(_sm.attackScript.isAttacking))
        { 
            stateMachine.ChangeState(_sm.idleState);
        }
        if(!(_sm.attackScript.isAttacking)&& _sm.currentDistance > _sm.closestDistance)
        {
            stateMachine.ChangeState(_sm.movingState);
        }
    }

}
