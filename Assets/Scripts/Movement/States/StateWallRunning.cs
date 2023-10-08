using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class StateWallRunning : AbstractState
{
    private Player.MoveStates previousState;
    private GameObject playerBody;
    private Rigidbody playerRigidbody;
    private int counter = 1;
    private int counterMax = 1000;

    public StateWallRunning(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
    }
    public override void OnEnter()
    {
        Debug.Log("Current State : Wallrunning");
        PlayerSetup();

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        Timer();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

    }
    public override void OnExit()
    {
        OwnerData.Delete("previousState");
        previousState = Player.MoveStates.WallRunning;
        OwnerData.Write("previousState", previousState);
    }


    private void PlayerSetup()
    {
        playerBody = OwnerData.Read<GameObject>("playerDataPrefab");
        playerRigidbody = playerBody.GetComponent<Rigidbody>();
        playerRigidbody.constraints = 
              RigidbodyConstraints.FreezeRotationZ 
            | RigidbodyConstraints.FreezeRotationX 
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezePositionY;
    }
    private void Timer()
    {


        if (counter <= counterMax)
        {
            counter++;
        }
        else
        {
            TurnOnYPos();
            SwitchToWalking();
            ResetTimer();
        }
    }
    private void ResetTimer()
    {
        counter = 0;
    }
    private void TurnOnYPos()
    {
        playerRigidbody.constraints =
              RigidbodyConstraints.FreezeRotationZ
            | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY;
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
