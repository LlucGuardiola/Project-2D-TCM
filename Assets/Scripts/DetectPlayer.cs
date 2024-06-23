using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject wallR;
    [SerializeField] private GameObject wallL;
    [SerializeField] private  GameObject boss;
    [SerializeField] private Animator animator;
    private GameObject player;


    // Start is called before the first frame update

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss.SetActive(false);
        wallR.SetActive(false);
        wallL.SetActive(false);
    }

    private void Update()
    {
        if (boss.GetComponent<HealthManager>().vidas_Boss <= 0)
        {
            Destroy(wallR);
            Destroy(wallL);
            Destroy(this);
            boss.GetComponent<HealthManager>().HideHealthBar();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        wallR.SetActive(true);
        wallL.SetActive(true);
        boss.SetActive(true);
        boss.GetComponent<HitController>().anim = player.GetComponent<Animator>();
        player.GetComponent<HitController>().anim = animator;
        boss.GetComponent<HealthManager>().ShowHealthBar();
    }
   
}
