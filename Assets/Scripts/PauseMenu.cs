using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
   public static bool GameIsPaused = false;
   public GameObject PauseMenUI;


    void Update()
    {
        if  (Input.GetKeyDown(KeyCode.Escape))
        {
            GameIsPaused = !GameIsPaused;

            Debug.Log(GameIsPaused);

            if (GameIsPaused)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    void Continuar ()
    {
        PauseMenUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;

    }

    void Pausar ()
    {
        PauseMenUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
