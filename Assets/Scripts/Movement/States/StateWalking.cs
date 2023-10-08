using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateWalking : AbstractState
{
    private StateMachine stateMachine;

    private Rigidbody playerRigidbody;
    private GameObject TempPlayer;

    private enum Movestates { beginWalkState, walkState, endWalkState }
    private Movestates currentState;

    float walkForce = 10.0f;

    private bool KeyW;
    private bool KeyA;
    private bool KeyS;
    private bool KeyD;


    public StateWalking(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
        stateMachine = _ownerStateMachine;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Switched to Jumping");
        PlayerSetup();
        currentState = Movestates.endWalkState;
        Debug.Log(currentState);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        SwitchWalkStates();

        if (Input.GetKey(KeyCode.W))
        {
            KeyW = true;
        }
        else 
        {
            KeyW = false;
        }
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    KeyA = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.A))
        //{
        //    KeyA = false;
        //}

        //else if (Input.GetKey(KeyCode.S))
        //{
        //    KeyS = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.S))
        //{
        //    KeyS = false;
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    KeyD = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.D))
        //{
        //    KeyD = false;
        //}

    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (KeyW == true)
        {
            WalkForward();
        }
        else if (KeyA == true)
        {
            WalkLeft();
        }
        else if (KeyS == true)
        {
            WalkBackward();
        }
        else if (KeyD == true)
        {
            WalkRight();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        GameObject.Destroy(TempPlayer);
    }
    private void PlayerSetup()
    {
        TempPlayer = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        TempPlayer.transform.position = new Vector3(4, 1, 4);
        TempPlayer.AddComponent<Rigidbody>();
        playerRigidbody = TempPlayer.GetComponent<Rigidbody>();
        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }

    private void SwitchWalkStates()
    {
        if (playerRigidbody.velocity.magnitude < 1 && playerRigidbody.velocity.magnitude > 0 && currentState == Movestates.walkState)
        {
            //Debug.Log(playerRigidbody.velocity.magnitude);

            currentState = Movestates.endWalkState;
            SwitchtoStanding();
            Debug.Log(currentState);
        }
        if (playerRigidbody.velocity.magnitude < 1 && playerRigidbody.velocity.magnitude > 0 && currentState == Movestates.endWalkState)
        {
            //Debug.Log(playerRigidbody.velocity.magnitude);

            currentState = Movestates.beginWalkState;
            Debug.Log(currentState);

        }
        if (playerRigidbody.velocity.magnitude > 5)
        {
            //Debug.Log(playerRigidbody.velocity.magnitude);

            currentState = Movestates.walkState;
            Debug.Log(currentState);

        }
    }


    private void WalkForward()
    {
        playerRigidbody.AddRelativeForce(Vector3.forward * walkForce);
    }
    private void WalkBackward()
    {
        playerRigidbody.AddRelativeForce(Vector3.back * walkForce);

    }
    private void WalkLeft()
    {
        playerRigidbody.AddRelativeForce(Vector3.left * walkForce);

    }

    private void WalkRight()
    {
        playerRigidbody.AddRelativeForce(Vector3.right * walkForce);

    }

    private void SwitchtoStanding()
    {
        stateMachine.SwitchState(typeof(StateStanding));
    }
}
