using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestsEnemy : MonoBehaviour
{
    private bool dead = false;
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;  
    }
    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        animator.SetTrigger("hit");
        Debug.Log(currentHealth);
        if (currentHealth <= 0) { Die(); } 
    }
    void Die()
    {
        dead = true;
        animator.SetBool("dead",dead);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f);
    }
   
}
