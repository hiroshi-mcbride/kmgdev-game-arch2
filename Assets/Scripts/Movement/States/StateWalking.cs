using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalking : AbstractState
{
    private Rigidbody playerRigidbody;
    private GameObject playerBody;
    private Transform camTransform;
    private Player.MoveStates previousState;

    private enum Walkstates { beginWalkState, walkState, endWalkState }
    private Walkstates currentState;

    float walkForce = 10.0f;

    private bool KeyW;
    private bool KeyA;
    private bool KeyS;
    private bool KeyD;

    private float sensX;
    private float sensY;

    private float xRotation;
    private float yRotation;

    private int WKey;
    private int AKey;
    private int SKey;
    private int DKey;


    // variable voor de nieuwe playerController




    public StateWalking(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        PlayerSetup();
        currentState = Walkstates.endWalkState;
        //camTransform = playerBody.transform.GetChild(0);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        SwitchWalkStates();
        CheckInput();
        //RotateCamera();

    }

    public override void OnFixedUpdate()
    {
        Walk();
    }

    public override void OnExit()
    {
        OwnerData.Delete("previousState");
        previousState = Player.MoveStates.Walking;
        OwnerData.Write("previousState", previousState);
    }
    private void PlayerSetup()
    {
        playerBody = OwnerData.Read<GameObject>("playerDataPrefab");
        playerRigidbody = playerBody.GetComponent<Rigidbody>();
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    //Movement
    private void CheckInput()
    {
        WKey = CalculateInput(KeyCode.W);
        AKey = CalculateInput(KeyCode.A);
        SKey = CalculateInput(KeyCode.S);
        DKey = CalculateInput(KeyCode.D);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchToJumping();
        }
    }
    private int CalculateInput(KeyCode _keyCode)
    {
        int value;
        if (Input.GetKey(_keyCode))
        {
            if (_keyCode == KeyCode.W || _keyCode == KeyCode.D)
            {
                value = 1;
            }
            else
            {
                value = -1;
            }
        }
        else
        {
            value = 0;
        }
        return value;

    }
    private void Walk()
    {
        float zAxe;
        float xAxe;

        xAxe = AKey + DKey;
        zAxe = WKey + SKey;
        Vector3 test = new Vector3(xAxe, 0 ,zAxe);
        //Debug.Log(test);

        playerRigidbody.AddRelativeForce(new Vector3(xAxe, 0, zAxe) * walkForce);
    }
    
    // Switching States
    private void SwitchWalkStates()
    {
        if (playerRigidbody.velocity.magnitude < 1 && playerRigidbody.velocity.magnitude > 0 && currentState == Walkstates.walkState)
        {
            currentState = Walkstates.endWalkState;
            SwitchtoStanding();
        }
        if (playerRigidbody.velocity.magnitude < 1 && playerRigidbody.velocity.magnitude > 0 && currentState == Walkstates.endWalkState)
        {
            currentState = Walkstates.beginWalkState;
        }
        if (playerRigidbody.velocity.magnitude > 5)
        {
            currentState = Walkstates.walkState;
        }
    }
    private void SwitchtoStanding()
    {
        OwnerStateMachine.SwitchState(typeof(StateStanding));
    }
    private void SwitchToJumping()
    {
        OwnerStateMachine.SwitchState(typeof(StateJumping));

    }
}
