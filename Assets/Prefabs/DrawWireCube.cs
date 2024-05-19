using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawWireCube : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    private GameObject player;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        audioSource.volume = 0.08f;
    }

    private void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = Color.green;

            Vector3 center = new Vector3(boxCollider.offset.x, boxCollider.offset.y, transform.position.z);
            Vector3 size = new Vector3(boxCollider.size.x, boxCollider.size.y, 0f);

            Gizmos.DrawWireCube(center + transform.position, size);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.Play();
            player.GetComponent<PlayerAttack>().SetCombatState(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.Stop();
            player.GetComponent<PlayerAttack>().SetCombatState(false);
           
           
        }
    }
   

}
