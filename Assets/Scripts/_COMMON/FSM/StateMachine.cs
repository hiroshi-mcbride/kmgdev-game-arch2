using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : IUpdateable
{
    private Dictionary<Type, IState> states = new();
    private IState currentState;

    public bool IsActive { get; set; } = true;


    public StateMachine(params IState[] _states)
    {
        EventManager.Invoke(new UpdateableCreatedEvent(this));

        foreach (IState s in _states)
        {
            states.TryAdd(s.GetType(), s);
        }
    }

    public void Update()
    {
        currentState?.OnUpdate();
    }

    public void FixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }

    public void AddState(IState _state)
    {
        states.TryAdd(_state.GetType(), _state);
    }

    public void SwitchState(Type _newState)
    {
        currentState?.OnExit();
        currentState = states[_newState];
        currentState?.OnEnter();
    }

    ~StateMachine()
    {
        currentState?.OnExit();
    }
}
