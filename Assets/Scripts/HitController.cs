using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class HitController
    : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask layermask;
    [SerializeField] private Transform attackPoint;

    private void Update()
    {
           attackPoint.position = new Vector2(
          transform.position.x + (GetComponent<SpriteRenderer>().flipX ? -3.5f : 3.5f),
          transform.position.y);
    }
    private void Hit() // Activated within animation /// Canviar
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layermask);  //Detectar enemigos en un rango especificado
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy is CapsuleCollider2D)
            {
                //////arreglar
                Debug.Log("daño");
            }
        }
    }
    private void OnDrawGizmosSelected()  //Dibujar esfera para ver rango de ataque
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
