using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Moving : BaseState
{
    
    private MovementSM _sm;
   

    public Moving(MovementSM stateMachine) : base("Moving", stateMachine) 
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
       
        // Transiton to "Idle" state if position = 0

        if (!(_sm.currentDistance > _sm.closestDistance))
        { 
            stateMachine.ChangeState(_sm.idleState);
        }
        Follow();
    }

  
    public void Follow()
    {

       
       Vector2 newPosition = Vector2.MoveTowards(_sm.transform.position, _sm.player.transform.position, 10 * Time.deltaTime);
       _sm.transform.position = new Vector2(newPosition.x, _sm.transform.position.y);
    }
   
}
