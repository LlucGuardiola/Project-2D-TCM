using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack_Controller : MonoBehaviour
{
    
    
    private float attackCooldown;

    private bool canAttack = true;
    [HideInInspector] public bool isAttacking = false;

    private float end;
    private float attackTimeLeft;

    private Animator animator;
    private string[] animations;
  
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
    public void TryStartAttack(float attackCooldown, string [] attackAnim, Animator animator,float duration)
    {
        if (canAttack && !isAttacking)
        {
            this.attackCooldown = attackCooldown;
            animations = attackAnim;
            this.animator = animator;
            end = duration;
            StartAttack();
        }
    }
    void StartAttack()
    {
        int i = UnityEngine.Random.Range(0, animations.Length); 
        animator.SetTrigger(animations[i]);
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
