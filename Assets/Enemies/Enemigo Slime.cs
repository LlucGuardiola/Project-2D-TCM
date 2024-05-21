using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSlime : MonoBehaviour
{
    [SerializeField] private bool RedSlime;
    [SerializeField] private bool GreenSlime;
    [SerializeField] private bool GraySlime;
    [SerializeField] private bool BlueSlime;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("RedSlime", RedSlime);
        animator.SetBool("GreenSlime", GreenSlime);
        animator.SetBool("GraySlime", GraySlime);
        animator.SetBool("BlueSlime", BlueSlime);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
         {
            collision.gameObject.GetComponent<PlayerMovement>().AplicarGolpe();
         }
    }
}
