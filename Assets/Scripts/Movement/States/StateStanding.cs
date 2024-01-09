using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStanding : AbstractState
{
    private StateMachine stateMachine;
    private Scratchpad PlayerData;

    private Player.MoveStates previousState;
   
    

    //public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public StateStanding(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
        stateMachine = _ownerStateMachine;
        PlayerData = _ownerData;


    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        CheckInput();


    }
    public override void OnExit()
    {
        OwnerData.Delete("previousState");
        previousState = Player.MoveStates.Standing;
        OwnerData.Write("previousState", previousState);
    }

    private void CheckInput()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            SwitchToWalking();
        }
        if (Input.GetKey(KeyCode.A))
        {
            SwitchToWalking();
        }
        if (Input.GetKey(KeyCode.S))
        {
            SwitchToWalking();
        }
        if (Input.GetKey(KeyCode.D))
        {
            SwitchToWalking();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchToJumping();
        }
    }


    //SwitchStates
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
        stateMachine.SwitchState(typeof(StateJumping));
    }




}
