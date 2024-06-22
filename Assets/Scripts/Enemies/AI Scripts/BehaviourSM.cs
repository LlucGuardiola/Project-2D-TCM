using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSM : StateMachine
{
    [HideInInspector]public Idle_State idleState;
    [HideInInspector] public Moving_State movingState;
    [HideInInspector] public Attack_State AttackState;


    [HideInInspector] public float closestDistance;
    [HideInInspector] public float currentDistance;
    [HideInInspector] public Transform player;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Attack_Tests attackScript;
    [HideInInspector] public string [] attack_Anim;

    public BoxCollider2D bossArea;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 worldSize = Vector3.Scale(player.GetComponent<BoxCollider2D>().size, transform.localScale);
        closestDistance = worldSize.x *1.2f ;
        idleState = new Idle_State(this);
        movingState = new Moving_State(this);
        AttackState = new Attack_State(this);
        animator = GetComponent<Animator>();
        attackScript = GetComponent<Attack_Tests>();
        attack_Anim = new string[] { "Attack", "Attack2" };
        base.Start();
    }
    
    protected override BaseState GetInitialState()
    {
        return idleState;
    }

}
