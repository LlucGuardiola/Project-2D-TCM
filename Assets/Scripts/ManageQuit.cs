using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageQuit : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

    }
    void Update()
    {
        if (player.GetComponent<HealthManager>().LifeCounter == 3)
        {
            Application.Quit();
        }
    }
}
