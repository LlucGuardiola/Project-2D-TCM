using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance { get; private set; }
    public GameObject PauseMenUI;
    public GameObject DeadUI;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
           
        }
     
        
        _currentState = GameState.Playing;
    }
    

    public enum GameState
    {
        PreGame,
        Playing,
        Paused,
        Finished
    }

    private GameState _currentState;
    public GameState CurrentState
    {
        get { return _currentState; }
    }

    public void PauseGame()
    {
        _currentState = GameState.Paused;
        PauseMenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        if(_currentState == GameState.Finished)
        {
            SceneManager.LoadScene("1_Aimar");
        }
        _currentState = GameState.Playing;
        PauseMenUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public bool ShouldPause()
    {
        return _currentState == GameState.Playing;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pausa");
         
            
            if (ShouldPause())
            {
                Debug.Log("Pausa");
                PauseGame();
            }
            else if (_currentState == GameState.Paused)
            {
                Debug.Log("No pausa");
                UnPauseGame();
            }
        }
       
        if (player.GetComponent<HealthManager>().LifeCounter == 1)/////////////
        {
            _currentState = GameState.Finished;
            Finished();
        }
    }

    public void Finished()
    {
            DeadUI.SetActive(true);    
    }
}
