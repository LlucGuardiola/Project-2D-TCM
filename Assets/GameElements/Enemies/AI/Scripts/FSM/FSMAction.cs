using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMAction : ScriptableObject
{
    public virtual void OnEnter(BaseStateMachine stateMachine) { }
    public virtual void OnExit(BaseStateMachine stateMachine) { }

    public abstract void Execute(BaseStateMachine stateMachine);
}
