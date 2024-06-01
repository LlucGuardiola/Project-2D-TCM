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
    private bool isDashing = false;
    private float dashingTime = 0.2f;
    private float dashTimeLeft;
    private float dashCooldownTime;
    private Rigidbody2D rigidBody;
    private TrailRenderer tr;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }
    void Update()
    {
        if (isDashing)
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
        if (canDash && !isDashing)
        {
            this.dashingCooldown = dashingCooldown;
            this.dashingPower = dashingPower;
            this.dashForward = dashForward;
            StartDash();
        }
    }
    void StartDash()
    {
        canDash = false;
        isDashing = true;
        dashTimeLeft = dashingTime;

        float dashDirection = dashForward ? 1 : -1;
        dashDirection *= GetComponent<SpriteRenderer>().flipX ? -1 : 1;

        rigidBody.velocity = new Vector2(dashDirection * dashingPower, 0);
        tr.emitting = true;
    }
    void ContinueDash()
    {
        dashTimeLeft -= Time.deltaTime;

        if (dashTimeLeft <= 0)
        {
            EndDash();
        }
    }
    void EndDash()
    {
        isDashing = false;
        tr.emitting = false;
        dashCooldownTime = dashingCooldown;
        rigidBody.velocity = Vector2.zero; // Optional: Stop the player's movement after dash ends
    }
    void DashCooldown()
    {
        dashCooldownTime -= Time.deltaTime;

        if (dashCooldownTime <= 0)
        {
            canDash = true;
        }
    }
}
