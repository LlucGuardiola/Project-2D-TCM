using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJan : Boss
{
    // Espai per crear variables

    public BossJan(float vida, float damage) : base(vida, damage) { }

    void Start()
    {

    }

    public override void Update()
    {

    }

    public override float GetDamage() { return damage; }
    public override void MakeDamage(float damage) { vida -= damage; }
    public override float GetVida() { return vida; }
    public override void SetVida(float vida) { this.vida = vida; }
}
