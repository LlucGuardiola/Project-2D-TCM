using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dash : MonoBehaviour
{
    private bool dashForward;
    private float dashingPower;
    private float dashingCooldown;

    private bool canDash = true;
    public bool IsDashing = false;

    private float dashingTime = 0.2f;
    private float dashTimeLeft;
    private float originalGravity;
    public bool CanGetDamage { get; private set; } = true;


    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        originalGravity = rigidBody.gravityScale;
    }
    void Update()
    {
        if (IsDashing)
        {
            ContinueDash();
        }

        if (!canDash)
        {
            DashCooldown();
        }
    }
    public void TryStartDash(float dashingCooldown, float dashingPower, bool dashForward)
    {
        if (canDash && !IsDashing)
        {
            this.dashingCooldown = dashingCooldown;
            this.dashingPower = dashingPower;
            this.dashForward = dashForward;
            StartDash();
        }
    }
    void StartDash()
    {
        CanGetDamage = false;
        canDash = false;
        IsDashing = true;
        dashTimeLeft = dashingTime;

        float dashDirection = dashForward ? -1 : 1;
        dashDirection *= GetComponent<SpriteRenderer>().flipX ? -1 : 1;

        rigidBody.velocity = new Vector2(dashDirection * dashingPower, 0);
        rigidBody.gravityScale = 0f; 
    }
    void ContinueDash()
    {
        dashTimeLeft -= Time.deltaTime;
        if (dashTimeLeft < 0)
        {
            EndDash();
        }
    }
    void EndDash()
    {
        rigidBody.gravityScale = originalGravity;
        IsDashing = false;
        CanGetDamage = true;
        rigidBody.velocity = Vector2.zero;
    }
    void DashCooldown()
    {
        dashingCooldown -= Time.deltaTime;
        if (dashingCooldown < 0)
        {
            dashingCooldown = 0;
            canDash = true;
        }
    }
}