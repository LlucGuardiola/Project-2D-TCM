using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    protected float vida;
    protected float damage;

    public Boss(float vida, float damage)
    {
        this.vida = vida;
        this.damage = damage;
    }

    public abstract void Update();
    public abstract void SetVida(float vida);
    public abstract float GetVida();
    public abstract void MakeDamage(float damage);
    public abstract float GetDamage();
}
