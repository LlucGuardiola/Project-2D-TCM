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

        Debug.Log(currentPosition + " - " + transform.position);
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

        if (!(currentDistance < closestDistance))
        {
            Vector2 newTranform = Vector2.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
            transform.position = new Vector2(newTranform.x, transform.position.y);
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
