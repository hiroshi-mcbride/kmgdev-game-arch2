using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class StateStanding : AbstractState
{
    private StateMachine stateMachine;
    private Scratchpad PlayerData;

    public int Id => throw new NotImplementedException();

    public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public StateStanding(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
        stateMachine = _ownerStateMachine;
        PlayerData = _ownerData;
    }
    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            SwitchToJumping();
        }



    }

    public override void OnExit()
    {
    }


    private void SwitchToWalking()
    {
        stateMachine.SwitchState(typeof(StateWalking));
    }

    private void SwitchToRunning()
    {
        stateMachine.SwitchState(typeof(StateRunning));
    }
    private void SwitchToJumping()
    {
        Console.WriteLine("Switch to Jumping");
        stateMachine.SwitchState(typeof(StateJumping));
    }




}
