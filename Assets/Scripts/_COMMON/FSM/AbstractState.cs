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

    public virtual void Enter()
    {
        Debug.Log($"Entered {this}");
    }
    public virtual void Update(float _delta) {}
    public virtual void FixedUpdate(float _fixedDelta) {}
    public virtual void Exit() {}
}
