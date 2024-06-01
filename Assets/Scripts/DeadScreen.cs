using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreen : MonoBehaviour
{
    public GameObject menu;

    private void Start()
    {
        GameFlowController.Instance.SetDeadScreen(menu);
    }

    public void Continuar()
    {
        GameFlowController.Instance.UnPauseGame();
    }
    public void Salir()
    {
        Application.Quit();
    }
}
