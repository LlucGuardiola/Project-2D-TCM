using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject wallR;
    [SerializeField] private GameObject wallL;
    [SerializeField] private  GameObject boss;
   

    // Start is called before the first frame update

    private void Start()
    {
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
            Destroy(boss);
            Destroy(this);
            boss.GetComponent<HealthManager>().HideHealthBar();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        wallR.SetActive(true);
        wallL.SetActive(true);
        boss.SetActive(true);
        boss.GetComponent<HealthManager>().ShowHealthBar();
    }
   
}
