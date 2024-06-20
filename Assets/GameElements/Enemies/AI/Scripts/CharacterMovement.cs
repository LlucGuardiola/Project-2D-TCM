using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;

    Vector2 lookAt;
    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        animator.SetFloat("LookAtX", lookAt.x);
        animator.SetFloat("LookAtY", lookAt.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();

        if (moveDirection.sqrMagnitude > 0.0f)
        {
            lookAt = moveDirection;
        }

        sprite.flipX = lookAt.x < 0;
    }

    public void OnAttack(InputValue value)
    {
        if(value.isPressed)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void OnDie(InputValue value)
    {
        animator.SetTrigger("Die");
    }
}
