using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask layermask_Player;
    [SerializeField] private LayerMask layermask_Boss;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Animator anim;
    private HealthManager healthManager;
    private bool isBoss;
    private GameObject _player;
    

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        isBoss = healthManager.isBoss;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Debug.Log(isBoss);
        if (isBoss)
        {
            attackPoint.position = new Vector2(
                transform.position.x + (GetComponent<SpriteRenderer>().flipX ? -3.5f : 3.5f),
                transform.position.y);
        }
        
    }

    private void Hit() // Activated within animation
    {
        if (isBoss)
        {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layermask_Player);
            foreach (Collider2D player in hitPlayer)
            {
                HealthManager playerHealthManager = player.GetComponent<HealthManager>();
                if (playerHealthManager != null)
                {
                    playerHealthManager.LoseLife(Random.Range(7,13), _player.GetComponent<Dash>().CanGetDamage);
                    Debug.Log("daño a jugador");
                    anim.SetTrigger("HitP");
                }
            }
        }
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layermask_Boss);
            foreach (Collider2D enemy in hitEnemies)
            {
                
                if (enemy is CapsuleCollider2D)
                {
                    HealthManager enemyHealthManager = enemy.GetComponent<HealthManager>();
                    if (enemyHealthManager != null)
                    {
                        enemyHealthManager.LoseLifeBoss(20, true);
                        Debug.Log("daño a boss");
                        anim.SetTrigger("HitB");
                    }
                }
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

