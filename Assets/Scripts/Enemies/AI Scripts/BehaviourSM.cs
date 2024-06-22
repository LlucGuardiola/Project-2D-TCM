using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSM : StateMachine
{
    [HideInInspector]public Idle idleState;
    [HideInInspector] public Moving movingState;
    [HideInInspector] public Attack_State AttackState;


    [HideInInspector] public float closestDistance;
    [HideInInspector] public float currentDistance;
    [HideInInspector] public Transform player;
    [HideInInspector] public Animator animator;
    public BoxCollider2D bossArea;

    protected override void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 worldSize = Vector3.Scale(player.GetComponent<BoxCollider2D>().size, transform.localScale);
        closestDistance = worldSize.x *1.2f ;
        idleState = new Idle(this);
        movingState = new Moving(this);
        AttackState = new Attack_State(this);
        animator = GetComponent<Animator>();
        base.Start();
    }
    
    protected override BaseState GetInitialState()
    {
        return idleState;
    }

}
