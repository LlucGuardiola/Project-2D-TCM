using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
<<<<<<< HEAD
    protected float vida;
=======
    [SerializeField] protected float vida;
    [SerializeField] protected GameObject player;
    [SerializeField] protected AnimationClip deathAnim;
    [SerializeField] protected BoxCollider2D bossArea;

    protected float closestDistance;
    protected float currentDistance;
    protected Vector3 currentPosition;
    protected bool dead = false;
>>>>>>> c8ddc7067d45fda73e9fee55602a47c9a32aa4a3
    protected float damage;

    public Boss(float vida, float damage)
    {
        this.vida = vida;
        this.damage = damage;
    }

<<<<<<< HEAD
    public abstract void Update();
    public abstract void SetVida(float vida);
    public abstract float GetVida();
    public abstract void MakeDamage(float damage);
    public abstract float GetDamage();
=======
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
>>>>>>> c8ddc7067d45fda73e9fee55602a47c9a32aa4a3
}
