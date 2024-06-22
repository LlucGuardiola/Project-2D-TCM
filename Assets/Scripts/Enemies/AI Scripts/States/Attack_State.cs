using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Attack_State : BaseState
{

    private BehaviourSM _sm;
    private bool isAttacking;
    private float ts;
    private float end;


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
        if ((!isAttacking) && _sm.currentDistance < _sm.closestDistance)
        {
            AttackAnimation();
        }
        
        if (isAttacking )
        {
            UpdateAttack();
        }
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
       
        // Transiton to "Idle" state if position = 0

        if (!(isAttacking))
        { 
            stateMachine.ChangeState(_sm.idleState);
        }
        if(!(isAttacking)&& _sm.currentDistance > _sm.closestDistance)
        {
            stateMachine.ChangeState(_sm.movingState);
        }
    }

    private void AttackAnimation() // Activated within the animation
    {
        if(!isAttacking)
        {
            end = 2;
            isAttacking = true;
            ts = 0f;
        }
        Debug.Log("starting attack");
    }

    private void UpdateAttack()
    {

        ts += Time.deltaTime;

        if(ts >= end)
        {
            isAttacking=false;
        }
    }

}
