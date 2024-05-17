using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Boss : MonoBehaviour
{
    [SerializeField] protected float vida;
    [SerializeField] protected GameObject player;
    [SerializeField] protected AnimationClip deathAnim;
    [SerializeField] protected BoxCollider2D bossArea;

    protected float closestDistance;
    protected float currentDistance;
    protected Vector3 currentPosition;
    protected bool dead = false;
    protected float damage;
    protected Animator animator;

    protected bool canDash = true;
    protected bool isDashing = false;

    public float attackRange;
    public LayerMask playerLayer;    // Capa de enemigos
    private bool isAtacking = false;
    protected float nextAttacktime = 0f;
    [SerializeField] protected Transform attackPoint;    //Punto para rango de ataque


    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        closestDistance = player.GetComponent<SpriteRenderer>().sprite.bounds.size.x + GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }
    public Boss(float vida, float damage)
    {
        this.vida = vida;
        this.damage = damage;
    }
    public virtual void Update()
    {
        currentDistance = (player.transform.position - transform.position).magnitude;

        Attack();

        animator.SetBool("isRunning", currentPosition != transform.position);

        Follow();
    }
    public virtual void SetVida(float vida) { }
    public virtual float GetVida() { return vida; }
    public virtual void MakeDamage(float damage) { }
    public virtual float GetDamage() { return damage; }
    public virtual void Follow() 
    {
        currentPosition = transform.position;

        if (player.transform.position.x < transform.position.x) { GetComponent<SpriteRenderer>().flipX = true; }
        else { GetComponent<SpriteRenderer>().flipX = false; }

        if (!(currentDistance < closestDistance) && CanFollow() && !isAtacking && !isDashing)
        {
            Vector2 newTranform = Vector2.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
            transform.position = new Vector2(newTranform.x, transform.position.y);
        }
    }
    private bool CanFollow()
    {
        Vector2 newTranform = Vector2.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
        
        if (newTranform.x < transform.position.x)
        {
            if (transform.position.x - 1 < bossArea.transform.position.x - (bossArea.size.x / 2)) { return false; }
            else { return true; }
        }
        else
        {
            if (transform.position.x + 1 > bossArea.transform.position.x + (bossArea.size.x / 2)) { return false; }
            else { return true; }
        }
    }
    public virtual void TakeDamage(int Damage)
    {
        vida -= Damage;
        if (vida <= 0) 
        { 
            Die(); 
        }

        StartCoroutine(Dash(0.2f, 20));
        animator.SetTrigger("Hit");
    }
    private IEnumerator Dash(float dashingTime, float dashingPower)
    {
        // tr.emitting = true;

        float end = Time.time + dashingTime;
        isDashing = true;

        while (Time.time < end)
        {
            transform.position = GetComponent<SpriteRenderer>().flipX ?
                new Vector3(transform.position.x +  1 * dashingPower * Time.deltaTime, transform.position.y, 0) :
                new Vector3(transform.position.x + -1 * dashingPower * Time.deltaTime, transform.position.y, 0);

            yield return null;
        }

        yield return new WaitForSeconds(1);
        Debug.Log("Test test test test su puta mAAAAAAAAdreeeeeee");
        isDashing = false;
    }
    public virtual void Die()
    {
        dead = true;
        animator.SetBool("Dead", dead);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        Destroy(gameObject, deathAnim.length);
    }
    public void Attack()
    {
        if (IsPlayerNearEnemy() && Time.time >= nextAttacktime)
        {
            animator.SetTrigger("Attack");
            nextAttacktime = Time.time + Random.Range(1,3);
        }

        if (!GetComponent<SpriteRenderer>().flipX)
        {
            attackPoint.transform.position = new Vector2(transform.position.x + 3.5f, transform.position.y + 1.1f);
        }
        else
        {
            attackPoint.transform.position = new Vector2(transform.position.x - 3.5f, transform.position.y + 1.1f);
        }
    }
    bool IsPlayerNearEnemy()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= attackRange * 2;
    }
    private void OnDrawGizmosSelected()  //Dibujar esfera para ver rango de ataque
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void Hit() // Activated within animation
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);  //Detectar enemigos en un rango especificado

        foreach (Collider2D player in hitPlayer)
        {
            if (player.GetComponent<PlayerMovement>().CanGetDamaged)
            {
                player.GetComponent<PlayerMovement>().LoseLife(10); /////////////////////////////////////////////////
            }
        }
    }
    private void SwitchAttacking() // Activated within animation
    {
        isAtacking = !isAtacking;
    }
}
