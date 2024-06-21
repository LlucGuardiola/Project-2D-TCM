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
        Debug.Log("Entered Moving State");

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Transiton to "Idle" state if position = 0
        if (_sm.transform.position == _sm.previousPosition)
        {

            stateMachine.ChangeState(_sm.idleState);

        }

        _sm.previousPosition = _sm.transform.position;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Follow();
    }

    public virtual void Follow()
    {

        if (_sm.currentDistance > _sm.bossArea.size.x / 2) return;

        _sm.GetComponent<SpriteRenderer>().flipX = _sm.player.transform.position.x < _sm.transform.position.x;

        if (IsInsideBossArea() && _sm.currentDistance > _sm.closestDistance)
        {
            Vector2 newPosition = Vector2.MoveTowards(_sm.transform.position, _sm.player.transform.position, 10 * Time.deltaTime);
            _sm.transform.position = new Vector2(newPosition.x, _sm.transform.position.y);
        }
    }
    private bool IsInsideBossArea()
    {
        float bossAreaMinX = _sm.bossArea.transform.position.x - (_sm.bossArea.size.x / 2);
        float bossAreaMaxX = _sm.bossArea.transform.position.x + (_sm.bossArea.size.x / 2);

        return _sm.transform.position.x > bossAreaMinX && _sm.transform.position.x < bossAreaMaxX;
    }
}
