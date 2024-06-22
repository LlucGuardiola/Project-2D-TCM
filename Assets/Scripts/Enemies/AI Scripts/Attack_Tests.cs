using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Tests : MonoBehaviour
{
    
    
    private float attackCooldown;

    private bool canAttack = true;
    [HideInInspector] public bool isAttacking = false;

    private float end = 0.2f;
    private float attackTimeLeft;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();    
    }
    void Update()
    {
        if (isAttacking)
        {
            ContinueAttack();
        }

        if (!canAttack)
        {
            AttackCooldown();
        }
    }
    public void TryStartAttack(float attackCooldown)
    {
        if (canAttack && !isAttacking)
        {
            this.attackCooldown = attackCooldown;
            
            StartAttack();
        }
    }
    void StartAttack()
    {
        Debug.Log("start attack");
        string attackTrigger = UnityEngine.Random.Range(0, 2) == 0 ? "Attack" : "Attack2";
        animator.SetTrigger(attackTrigger);
        canAttack = false;
        isAttacking = true;
        attackTimeLeft = end;
    }
    void ContinueAttack()
    {
        attackTimeLeft -= Time.deltaTime;
        if (attackTimeLeft < 0)
        {
            EndAttack();
        }
    }
    void EndAttack()
    {
        isAttacking = false;
    }
    void AttackCooldown()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0)
        {
            attackCooldown = 0;
            canAttack = true;
        }
    }
}
