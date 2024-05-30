using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Continuar ()
    {
       GameFlowController.Instance.UnPauseGame();
    }
    public void Salir()
    {
        Application.Quit();
    }
}
