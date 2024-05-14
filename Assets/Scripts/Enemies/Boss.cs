using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] protected float vida;
    [SerializeField] protected GameObject player;
    protected float closestDistance;
    protected bool dead = false;
    protected float damage;

    private void Start()
    {
        closestDistance = (player.GetComponent<BoxCollider2D>().size.x + GetComponent<CapsuleCollider2D>().size.x) * 2;
        Debug.Log(closestDistance);
    }
    public Boss(float vida, float damage)
    {
        this.vida = vida;
        this.damage = damage;
    }
    public virtual void Update()
    {
        if (!((player.transform.position - transform.position).magnitude < closestDistance))
        {
            Follow();
        }
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
        Vector2 newTranform = Vector2.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);

        transform.position = new Vector2(newTranform.x, transform.position.y);
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
        this.enabled = false;
        Destroy(gameObject, 1f);
    }
}
