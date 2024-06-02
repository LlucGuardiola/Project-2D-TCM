using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance { get; private set; }
    private GameObject pauseMenuUI;
    private GameObject deadUI;
    private GameState _currentState;
    private float playerLife;
    private void Start()
    {
       
        _currentState = GameState.PreGame;
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);

    }
    public void SetPauseMenu(GameObject pausemnu)
    {
        pauseMenuUI = pausemnu;
        pauseMenuUI.SetActive(false);
    }

    public void SetDeadScreen(GameObject deadUI)
    {
        this.deadUI = deadUI;
        deadUI.SetActive(false);
    }

    public void SetCounterLife(float life)
    {
        playerLife = life;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
    }
    public enum GameState
    {
        PreGame,
        Playing,
        Paused,
        Finished
    }
    public GameState CurrentState
    {
        get { return _currentState; }
    }
    public void PauseGame()
    {
        _currentState = GameState.Paused;
      
        Time.timeScale = 0f;
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
    }
    public void UnPauseGame()
    {
        if(_currentState == GameState.Finished)
        {
            SceneManager.LoadScene("GameFlowController");
           
        }
        _currentState = GameState.Playing;
        Time.timeScale = 1f;
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);

    }
    public bool ShouldPause()
    {
        return _currentState == GameState.Playing;
    }
    private void Update()
    {
        if (_currentState == GameState.Playing|| _currentState == GameState.Paused) 
        {
           
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (ShouldPause())
                {
                   
                    PauseGame();
                }
                else if (_currentState == GameState.Paused)
                {
                    UnPauseGame();
                }
            }
            if (playerLife== 1)
            {
                _currentState = GameState.Finished;
                Finished();
            }
        }             
    }
    
    public void Finished()
    {
        deadUI.SetActive(true);    
    }
    public void Play()
    {
        SceneManager.LoadSceneAsync("1_Aimar", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Menu");
        _currentState = GameState.Playing;
    }
}
