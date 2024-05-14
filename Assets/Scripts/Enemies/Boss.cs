using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Boss : MonoBehaviour
{
    [SerializeField] protected float vida;
    [SerializeField] protected GameObject player;
    protected float closestDistance;
    protected float currentDistance;
    protected bool dead = false;
    protected float damage;

    public virtual void Start()
    {
        closestDistance = player.GetComponent<SpriteRenderer>().sprite.bounds.size.x + GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        Debug.Log(closestDistance);
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
        if (player.transform.position.x < transform.position.x) { GetComponent<SpriteRenderer>().flipX = true; }
        else { GetComponent<SpriteRenderer>().flipX = false; }

        if (!(currentDistance < closestDistance))
        {
            Debug.Log(currentDistance);
            Vector2 newTranform = Vector2.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);

            transform.position = new Vector2(newTranform.x, transform.position.y);
        }
    }
    public virtual void TakeDamage(int Damage)
    {
        vida -= Damage;
        // animator.SetTrigger("hit");
        if (vida <= 0) { Die(); }
    }
    public virtual void Die()
    {
        dead = true;
        // animator.SetBool("dead", dead);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        Destroy(gameObject, 1f);
    }
}
