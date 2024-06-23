using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Input : MonoBehaviour
{
    private Attack_Controller attack_script;
    private string[] attack_Anim;
    private Animator animator;
    private Dash dash;

    private void Start()
    {
        attack_script = GetComponent<Attack_Controller>();
        animator = GetComponent<Animator>();
        dash = GetComponent<Dash>();
        attack_Anim = new string[] {"Attack"};
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GetComponent<Dash>().IsDashing && !GetComponent<PlayerMovement>().IsJumping)
        {
            
            dash.TryStartDash(1,6, true);
            
            attack_script.TryStartAttack(1, attack_Anim,animator, 0.5f);
        }
    }
}
