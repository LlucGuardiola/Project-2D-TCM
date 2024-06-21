using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.currentDistance = Vector3.Distance(_sm.player.transform.position, _sm.transform.position);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // Transiton to "moving" state
        Debug.Log(_sm.currentDistance);
        if (_sm.currentDistance > _sm.closestDistance)
        {
            
            stateMachine.ChangeState(_sm.movingState);
            
        }
        _sm.animator.SetBool("isRunning", false);
    }
    public bool IsInsideBossArea()
    {
        float bossAreaMinX = _sm.bossArea.transform.position.x - (_sm.bossArea.size.x / 2);
        float bossAreaMaxX = _sm.bossArea.transform.position.x + (_sm.bossArea.size.x / 2);
        return _sm.transform.position.x > bossAreaMinX && _sm.transform.position.x < bossAreaMaxX;
    }

}
