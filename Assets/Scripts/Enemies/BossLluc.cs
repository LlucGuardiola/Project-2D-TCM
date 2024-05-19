using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLluc : Boss
{
    // Espai per crear variables

    public BossLluc(float vida, float damage) : base(vida, damage) { }

<<<<<<< HEAD
    void Start()
    {

    }

    public override void Update()
    {

    }

=======
>>>>>>> c8ddc7067d45fda73e9fee55602a47c9a32aa4a3
    public override float GetDamage() { return damage; }
    public override void MakeDamage(float damage) { vida -= damage; }
    public override float GetVida() { return vida; }
    public override void SetVida(float vida) { this.vida = vida; }
}
