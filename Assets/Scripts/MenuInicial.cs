using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    
    public void Jugar ()
    {
        
        GameFlowController.Instance.Play();
    }

    public void Salir ()
    {
        Application.Quit();
    }
    
}
