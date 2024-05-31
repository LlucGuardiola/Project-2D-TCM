using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    private void Start()
    {
        menu.SetActive(false);
    }
    public void Continuar ()
    {
       GameFlowController.Instance.UnPauseGame();
    }
    public void Salir()
    {
        Application.Quit();
    }

    private void Update()
    {
        Debug.Log(GameFlowController.Instance._currentState);
        if (GameFlowController.Instance._currentState == GameFlowController.GameState.Paused)
        {
            menu.SetActive(true);

        }else 
        {
            menu.SetActive(false);
        }
        
    }
}
