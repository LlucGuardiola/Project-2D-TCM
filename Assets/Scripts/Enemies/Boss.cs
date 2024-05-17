using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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

        Follow();

        animator.SetBool("isRunning", currentPosition != transform.position);

        Attack();
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

        if (!(currentDistance < closestDistance) && CanFollow() && !isAtacking)
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
        StartCoroutine(Dash(0.2f, 1, 2));
        vida -= Damage;
        animator.SetTrigger("Hit");
        if (vida <= 0) { Die(); }
    }
    private IEnumerator Dash(float dashingTime, float dashingCooldown, float dashingPower)
    {
        Debug.Log("BOMBARDEEN TORREMOLINOS");
        // tr.emitting = true;

        canDash = true;
        float start = Time.time;
        float end = Time.time + 1;

        while (start != end && canDash)
        {
            transform.position = GetComponent<SpriteRenderer>().flipX == true ? new Vector3(transform.position.x + 1 * dashingPower * Time.deltaTime, transform.position.y, 0) :
                                                                            new Vector3(transform.position.x + -1 * dashingPower * Time.deltaTime, transform.position.y, 0);
            start = Time.time;
            yield return null;
        }
        canDash = false;
        yield return new WaitForSeconds(dashingCooldown + dashingTime);
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
