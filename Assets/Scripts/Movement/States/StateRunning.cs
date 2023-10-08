using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRunning : AbstractState
{
    private Player.MoveStates previousState;
    private GameObject playerBody;

    public StateRunning(Scratchpad _ownerData, StateMachine _ownerStateMachine): base(_ownerData, _ownerStateMachine)
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("Current State : Running");


    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchToJump();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SwitchToWalking();
        }

    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();



    }
    public override void OnExit()
    {
        OwnerData.Delete("previousState");
        previousState = Player.MoveStates.Running;
        OwnerData.Write("previousState", previousState);
    }

    private void SwitchToWalking()
    {
        OwnerStateMachine.SwitchState(typeof(StateWalking));
    }
    private void SwitchToJump()
    {
        OwnerStateMachine.SwitchState(typeof(StateJumping));
    }


}
