using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    [HideInInspector]public Idle idleState;
    [HideInInspector] public Moving movingState;
    [HideInInspector] public Vector3 previousPosition;

    [HideInInspector] public float currentDistance;
    [HideInInspector] public float closestDistance;
    public Transform player;
    public BoxCollider2D bossArea;

    protected override void Start()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
        previousPosition = transform.position;
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 worldSize = Vector3.Scale(player.GetComponent<BoxCollider2D>().size, transform.localScale);
        closestDistance = worldSize.x;
        base.Start();
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

}
