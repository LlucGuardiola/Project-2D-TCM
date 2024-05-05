using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool combatMode=false;         //Modo combate 
    public Animator animator;               //Referencia animator
    public Transform attackPoint;    //Punto para rango de ataque
    public LayerMask enemyLayers;    // Capa de enemigos

    public int attackDamage = 25;                 //rango de ataque
    public float attackRange = 0.5f;

    public float attackRate = 2f;                 //Cooldown attack
    float nextAttacktime = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))   // Activar modo Combate
        {
            combatMode = !combatMode;    
            if (combatMode)
            {
                animator.SetBool("Combat?", true);
                Debug.Log("Atcviated");
               
            }
            else
            {
                animator.SetBool("Combat?", false);
                Debug.Log("deactivate");
            }
        }
        if (combatMode)
        {
            if(Time.time >= nextAttacktime)   
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Attack();
                    nextAttacktime = Time.time + 1f / attackRate;
                }

            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");  //Animación 
        Debug.Log("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);  //Detectar enemigos en un rango especificado
       
        //Daño a enemigo
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<TestsEnemy>().TakeDamage(attackDamage);
        }

    }
    private void OnDrawGizmosSelected()   //Dibujar esfera para ver rango de ataque
    {
        if(attackPoint == null)
            return;
     
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
