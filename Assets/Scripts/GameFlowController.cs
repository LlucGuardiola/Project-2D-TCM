using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance { get; private set; }
    private GameObject PauseMenUI;
    private GameObject DeadUI;
    private GameObject player;
    private bool condition;
    public GameState _currentState;


    private void Start()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
        _currentState = GameState.PreGame;
        condition = true;
    }
    public void InstanceObjects()
    {
        player = GameObject.FindWithTag("Player");
        PauseMenUI = GameObject.FindWithTag("MenuPausa");
        DeadUI = GameObject.FindWithTag("DeadScreen");
    
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

     
    }

    public void UnPauseGame()
    {
        if(_currentState == GameState.Finished)
        {
            SceneManager.LoadScene("1_Aimar");
        }
        _currentState = GameState.Playing;
        Time.timeScale = 1f;

    }
    public bool ShouldPause()
    {
        return _currentState == GameState.Playing;
    }
    private void Update()
    { 
        if (_currentState == GameState.Playing|| _currentState == GameState.Paused) 
        {
            if(condition)
            {
                InstanceObjects();
            }
            if (Input.GetKeyDown(KeyCode.P))
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
            /*
            if (player.GetComponent<HealthManager>().LifeCounter == 1)/////////////
            {
                _currentState = GameState.Finished;
                Finished();
            }
            */
        }
       
    }
    /*
    public void Finished()
    {
            DeadUI.SetActive(true);    
    }
    */

    public void Play()
    {
        SceneManager.LoadSceneAsync("1_Aimar", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Menu");
        _currentState = GameState.Playing;
    }
}
