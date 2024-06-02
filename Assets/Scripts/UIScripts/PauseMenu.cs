using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;

    private void Start()
    {
        GameFlowController.Instance.SetPauseMenu(menu);
    }

    public void Continuar ()
    {
       GameFlowController.Instance.UnPauseGame();
    }
    public void Salir()
    {
        Application.Quit();
    }
}
