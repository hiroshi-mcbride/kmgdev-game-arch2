using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState : IState
{
    public Scratchpad OwnerData { get; }
    protected StateMachine OwnerStateMachine { get; }

    protected AbstractState(Scratchpad _ownerData, StateMachine _ownerStateMachine)
    {
        OwnerData = _ownerData;
        OwnerStateMachine = _ownerStateMachine;
    }

    public virtual void OnEnter()
    {
        Debug.Log($"Entered {this}");
    }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnExit() {}
}
