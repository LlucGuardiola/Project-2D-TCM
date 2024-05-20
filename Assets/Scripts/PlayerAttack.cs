using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
         //Modo combate 
    public Animator animator;               //Referencia animator
    public Transform attackPoint;    //Punto para rango de ataque
    public LayerMask enemyLayers;    // Capa de enemigos
    public GameObject player;

    public bool isAtacking = false;
    public int attackDamage = 25;                 //rango de ataque
    public float attackRange = 0.5f;
    public bool combatState;
    public float attackRate = 2;                 //Cooldown attack
    float nextAttacktime = 0f;

    public void SetCombatState(bool combatState)
    {
       this.combatState = combatState;
    }
    private void Update()
    {
        if (combatState)
        {
            animator.SetBool("Combat?", true);
        }
        else
        {
            animator.SetBool("Combat?", false);
        }

        if (combatState)
        {
            if(Time.time >= nextAttacktime)   
            {
                if (Input.GetMouseButtonDown(0) && !GetComponent<PlayerMovement>().IsDashing)
                {
                    Attack();
                    if(!GetComponent<PlayerMovement>().MovingRL) StartCoroutine(Dash(1000));
                    nextAttacktime = Time.time + attackRate / 2;
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
    private IEnumerator Dash(float dashingPower)
    {
        yield return null;
        yield return animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 2;

        float end = Time.time + animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 2;

        while (Time.time < end)
        {
            player.GetComponent<Rigidbody2D>().velocity = GetComponent<SpriteRenderer>().flipX == true ?
                                                          new Vector2( 1 * dashingPower * Time.deltaTime, 0.1f) :
                                                          new Vector2(-1 * dashingPower * Time.deltaTime, 0.1f);
            yield return null;
        }
    }
    private void OnDrawGizmosSelected()  //Dibujar esfera para ver rango de ataque
    {
        if(attackPoint == null)
            return;
     
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void AttackAnimation() { StartCoroutine(StartAttack()); } // Activated within the animation
    private IEnumerator StartAttack()
    {
        isAtacking = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * 1.1f);
        isAtacking = false;
    }
    private void Hit() // Activated within animation
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);  //Detectar enemigos en un rango especificado

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Boss>().TakeDamage(attackDamage);
        }
    }
}
