using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalking : AbstractState
{
    private StateMachine stateMachine;
    public StateWalking(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
        stateMachine = _ownerStateMachine;
    }

    private void TempUpdate()
    {
        if(Input.GetKeyUp(KeyCode.W))
        {
            WalkForward();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            WalkLeft();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            WalkBackward();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            WalkRight();
        }
        else
        {

        }
    }

    private void WalkForward()
    {

    }
    private void WalkBackward()
    {

    }
    private void WalkLeft()
    {

    }

    private void WalkRight()
    {

    }

    private void SwitchtoStanding()
    {
        stateMachine.SwitchState(typeof(StateStanding));
    }
}
