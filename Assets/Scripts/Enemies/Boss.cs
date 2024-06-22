using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] protected float vida;
    [SerializeField] protected AnimationClip deathAnim;
    [SerializeField] protected BoxCollider2D bossArea;

    protected GameObject player;
    protected float closestDistance;
    protected float currentDistance;
    protected Vector3 currentPosition;
    protected bool dead = false;
    protected float damage;
    protected Animator animator;

    private bool hasHit = false;
    protected bool isDashing = false;
    protected bool canDash = true;

    public float attackRange;
    public LayerMask playerLayer;
    private bool isAttacking = false;
    protected float nextAttackTime = 0f;
    [SerializeField] protected Transform attackPoint;

    public virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        Vector3 worldSize = Vector3.Scale(player.GetComponent<BoxCollider2D>().size, transform.localScale);
        closestDistance = worldSize.x;
    }

    public Boss(float vida, float damage)
    {
        this.vida = vida;
        this.damage = damage;
    }

    public virtual void Update()
    {
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetBool("isRunning", currentPosition != transform.position);

        Attack();
        Follow();
        ManageDash();
    }

    public virtual void Follow()
    {
        if (currentDistance > bossArea.size.x / 2) return;

        currentPosition = transform.position;

        GetComponent<SpriteRenderer>().flipX = player.transform.position.x < transform.position.x;

        if (IsInsideBossArea() && !isAttacking && !isDashing && currentDistance > closestDistance)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
            transform.position = new Vector2(newPosition.x, transform.position.y);
        }
    }

    private bool IsInsideBossArea()
    {
        float bossAreaMinX = bossArea.transform.position.x - (bossArea.size.x / 2);
        float bossAreaMaxX = bossArea.transform.position.x + (bossArea.size.x / 2);

        return transform.position.x > bossAreaMinX && transform.position.x < bossAreaMaxX;
    }

    public virtual void TakeDamage(int damage)
    {
        vida -= damage;
        if (vida <= 0) Die();

        StartCoroutine(Dash(0.2f, 10, false, 1));
        animator.SetTrigger("Hit");
    }

    private void ManageDash()
    {
        if (currentDistance > 10 && currentDistance < 30 && UnityEngine.Random.Range(0, 2) == 0 && canDash && !isAttacking)
        {
            StartCoroutine(Dash(0.2f, 25, true, 0));
            canDash = false;
            StartCoroutine(ResetDash(5));
        }
    }

    private IEnumerator ResetDash(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canDash = true;
    }

    private IEnumerator Dash(float dashingTime, float dashingPower, bool dashTowardsPlayer, float dashingCooldown)
    {
        isDashing = true;
        float end = Time.time + dashingTime;

        while (Time.time < end)
        {
            float dashDirection = dashTowardsPlayer ? 1 : -1;
            dashDirection *= GetComponent<SpriteRenderer>().flipX ? -1 : 1;
            transform.position += new Vector3(dashDirection * dashingPower * Time.deltaTime, 0, 0);
            yield return null;
        }

        yield return new WaitForSeconds(dashingCooldown);
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
        attackPoint.position = new Vector2(
            transform.position.x + (GetComponent<SpriteRenderer>().flipX ? -3.5f : 3.5f),
            transform.position.y + 1.1f
        );

        if (IsPlayerInRange() && Time.time >= nextAttackTime)
        {
            string attackTrigger = UnityEngine.Random.Range(0, 2) == 0 ? "Attack" : "Attack2";
            animator.SetTrigger(attackTrigger);
            nextAttackTime = Time.time + UnityEngine.Random.Range(1, 3.6f);
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= attackRange * 2;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

  
        
    public void ResetHit()
    {
        hasHit = false;
    }

    private void AttackAnimation() // Activated within the animation
    {
        StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        if (isAttacking) yield break;
        isAttacking = true;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        isAttacking = false;
        ResetHit();
    }
}