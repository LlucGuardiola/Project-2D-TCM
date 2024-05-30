using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.Compilation;
using UnityEngine;



public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        _currentState = GameState.Playing;
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
            if (ShouldPause())
            {
                PauseGame();
            }
            else if (_currentState == GameState.Paused)
            {
                UnPauseGame();
            }
        }
    }
}
