using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemigoSlime : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
         {
            collision.gameObject.GetComponent<PlayerMovement>().AplicarGolpe();
         }
    }
}
