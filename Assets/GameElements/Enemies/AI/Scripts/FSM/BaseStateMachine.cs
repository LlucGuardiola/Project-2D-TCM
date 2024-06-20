using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _initialState;
    private Dictionary<Type, Component> _cachedComponents;
    
    private void Awake()
    {
        _cachedComponents = new Dictionary<Type, Component>();
    }

    private BaseState _currentState = null;
    public BaseState CurrentState
    {
        get { return _currentState; }

        set
        {
            _currentState?.OnExit(this);
            _currentState = value;
            _currentState?.OnEnter(this);
        }
    }

    private void Start ()
    {
        CurrentState = _initialState;
    }

    private void Update()
    {
        CurrentState.Execute(this);
    }

    public new T GetComponent<T>() where T : Component
    {
        if(_cachedComponents.ContainsKey(typeof(T)))
            return _cachedComponents[typeof(T)] as T;

        var component = base.GetComponent<T>();
        if(component != null)
        {
            _cachedComponents.Add(typeof(T), component);
        }
        return component;
    }
}
