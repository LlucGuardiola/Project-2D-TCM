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

    private Rigidbody2D rigidBody;
    private TrailRenderer tr;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // tr = GetComponent<TrailRenderer>();
        originalGravity = rigidBody.gravityScale;
        ConfigureTrailRenderer(ref tr);
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
        canDash = false;
        IsDashing = true;
        dashTimeLeft = dashingTime;

        float dashDirection = dashForward ? -1 : 1;
        dashDirection *= GetComponent<SpriteRenderer>().flipX ? -1 : 1;

        rigidBody.velocity = new Vector2(dashDirection * dashingPower, 0);
        rigidBody.gravityScale = 0f; 
        tr.emitting = true;
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

        tr.emitting = false;
        rigidBody.velocity = Vector2.zero;
    }
    void DashCooldown()
    {
        dashingCooldown -= Time.deltaTime;
        Debug.Log(dashingCooldown);
        if (dashingCooldown < 0)
        {
            dashingCooldown = 0;
            canDash = true;
        }
    }
    void ConfigureTrailRenderer(ref TrailRenderer tr)
    {
        // Configurar el TrailRenderer con los valores deseados
        tr.time = 0.5f;
        tr.startWidth = 0.5f;
        tr.endWidth = 0.1f;
        tr.startColor = Color.white;
        tr.endColor = Color.red;
        tr.material = new Material(Shader.Find("Sprites/Default"));
        tr.minVertexDistance = 0.1f;
        tr.autodestruct = false;

        // Configuración de la gráfica (curve)
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 1.0f); // Al principio de la gráfica, el ancho es 1.0
        curve.AddKey(1.0f, 0.0f); // Al final de la gráfica, el ancho es 0.0
        tr.widthCurve = curve;

        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        tr.colorGradient = gradient;
    }
}
