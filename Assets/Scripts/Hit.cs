using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask layermask;
    [SerializeField] private Transform attackPoint;
    private void HitEntity() // Activated within the animation
    {
        Collider2D[] rangeCIrlce = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layermask);
        foreach (Collider2D hitEntity in rangeCIrlce)
        {
           hitEntity.GetComponent<HealthManager>().LoseLife(10, true);
        }
    }
}
