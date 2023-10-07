using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateJumping : AbstractState
{
    private bool IsGrounded;


    public StateJumping(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log(" Jumping");
        
    }

    private void Jump()
    {
        //rigidbodyAddForce
        // onEnter();

    }
}
