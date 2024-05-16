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
    protected Rigidbody2D rb;
    protected Animator animator;

    protected float attackRate = 2f;                 //Cooldown attack
    protected float nextAttacktime = 0f;
    protected Transform attackPoint;    //Punto para rango de ataque


    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = new Rigidbody2D();
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
    }
    public virtual void SetVida(float vida)
    {

    }
    public virtual float GetVida()
    {
        return vida;
    }
    public virtual void MakeDamage(float damage)
    {

    }
    public virtual float GetDamage()
    {
        return damage;
    }
    public virtual void Follow() 
    {
        currentPosition = transform.position;
        
        if (player.transform.position.x < transform.position.x) { GetComponent<SpriteRenderer>().flipX = true; }
        else { GetComponent<SpriteRenderer>().flipX = false; }

        if (!(currentDistance < closestDistance) && CanFollow())
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
        animator.SetTrigger("Hit");
        if (vida <= 0) { Die(); }
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
        if (Time.time >= nextAttacktime && (currentPosition != transform.position))
        {
            Attack();
            nextAttacktime = Time.time + 1f / attackRate;
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
