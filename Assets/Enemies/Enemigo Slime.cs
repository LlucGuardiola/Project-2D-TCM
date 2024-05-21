using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSlime : MonoBehaviour
{
    [SerializeField] private bool RedSlime;
    [SerializeField] private bool GreenSlime;
    [SerializeField] private bool GraySlime;
    [SerializeField] private bool BlueSlime;

    public float fuerzaGolpe;
    private Animator animator;
    private GameObject player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("RedSlime", RedSlime);
        animator.SetBool("GreenSlime", GreenSlime);
        animator.SetBool("GraySlime", GraySlime);
        animator.SetBool("BlueSlime", BlueSlime);
        player = GameObject.FindWithTag("Player");
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
         {
            AplicarGolpe();
         }
    }
    public void AplicarGolpe()
    {
        player.GetComponent<PlayerMovement>().CanMove = false;

        Vector2 direccion;

        if (player.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            direccion = new Vector2(-1, 1);
            player.GetComponent<Rigidbody2D>().AddForce(direccion * fuerzaGolpe);
        }
        else
        {
            direccion = new Vector2(1, 1);
            player.GetComponent<Rigidbody2D>().AddForce(direccion * fuerzaGolpe);
        }

        StartCoroutine(EsperarYActivarMovimiento());
    }
    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.4f);
        player.GetComponent<PlayerMovement>().CanMove = true;
        player.GetComponent<HealthManager>().LoseLife(10);
    }
}
