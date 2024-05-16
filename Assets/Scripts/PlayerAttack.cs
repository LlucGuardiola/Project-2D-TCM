using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool combatMode = false;         //Modo combate 
    public Animator animator;               //Referencia animator
    public Transform attackPoint;    //Punto para rango de ataque
    public LayerMask enemyLayers;    // Capa de enemigos
    public GameObject player;

    public bool isAtacking = false;
    public int attackDamage = 25;                 //rango de ataque
    public float attackRange = 0.5f;

    public float attackRate = 2f;                 //Cooldown attack
    float nextAttacktime = 0f;

    private void Update()
    {
        Debug.Log(isAtacking);
        if (Input.GetKeyDown(KeyCode.E))   // Activar modo Combate
        {
            combatMode = !combatMode;    
            if (combatMode)
            {
                animator.SetBool("Combat?", true);
                Debug.Log("Combat mode Activated");
               
            }
            else
            {
                animator.SetBool("Combat?", false);
                Debug.Log("Combat mode Deactivated");
            }
        }
        if (combatMode)
        {
            if(Time.time >= nextAttacktime && player.GetComponent<Rigidbody2D>().velocity.x == 0)   
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Attack();
                    nextAttacktime = Time.time + 1f / attackRate;
                }
            }
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                attackPoint.transform.position = new Vector2(player.transform.position.x + .8f, player.transform.position.y + 1.5f);
            }
            else
            {
                attackPoint.transform.position = new Vector2(player.transform.position.x - .8f, player.transform.position.y + 1.5f);
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");  //Animación 
    }
    private void OnDrawGizmosSelected()   //Dibujar esfera para ver rango de ataque
    {
        if(attackPoint == null)
            return;
     
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void SwitchAttacking() // Activated in animation
    {
        isAtacking = !isAtacking;
    }
    private void Hit() // Activated in animation
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);  //Detectar enemigos en un rango especificado

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<TestsEnemy>().TakeDamage(attackDamage);
        }
    }
}
