using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
       if ( collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<HealthManager>().LoseLife(20);
            collision.gameObject.GetComponent<ManageRespawn>().Respawn();
            Debug.Log("POLLA");

        }


        


    }



}
