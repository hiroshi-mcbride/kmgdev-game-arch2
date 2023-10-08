using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class StateWallRunning : AbstractState
{
    public enum WallRunStates { WallRunning, InAir }
    private WallRunStates currentSubState;
    private Player.MoveStates previousState;
    private GameObject playerBody;
    private Rigidbody playerRigidbody;
    private int counter = 1;
    private int counterMax = 1000;

    private float verticalJumpForce = 200.0f;
    private float horizontalJumpForce = 300.0f;
    private float ForwardpushForce = 30.0f;

    private bool goingRight;

    private int WKey;
    private int AKey;
    private int SKey;
    private int DKey;

    private Action<KeyWEvent> onKeyW;
    private Action<KeyAEvent> onKeyA;
    private Action<KeySEvent> onKeyS;
    private Action<KeyDEvent> onKeyD;
    private Action<KeySpaceEvent> onKeySpace;
    private Action<KeyLeftShiftEvent> onLeftShift;

    public StateWallRunning(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
    }
    public override void OnEnter()
    {
        LinkEvents();
        SubscribeEvents();
        currentSubState = WallRunStates.WallRunning;
        Debug.Log("Current State : Wallrunning");
        PlayerSetup();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        //if (currentSubState == WallRunStates.WallRunning)
        //{
        //    CheckInput();
        //}
        //else
        //{
        //    CheckForWalls();
        //}

        if (currentSubState == WallRunStates.WallRunning)
        {
            CheckForWalls();
        }
        Timer();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (currentSubState == WallRunStates.WallRunning)
        {
            CheckForWalls();
        }
    }
    public override void OnExit()
    {
        UnSubscribeEvents();
        OwnerData.Delete("previousState");
        previousState = Player.MoveStates.WallRunning;
        OwnerData.Write("previousState", previousState);
    }


    private void PlayerSetup()
    {
        playerBody = OwnerData.Read<GameObject>("playerDataPrefab");
        playerRigidbody = playerBody.GetComponent<Rigidbody>();
        TurnOffYPos();
    }


    //Movement
    private void TurnOnYPos()
    {
        playerRigidbody.constraints =
              RigidbodyConstraints.FreezeRotationZ
            | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY;
    }

    private void TurnOffYPos()
    {
        playerRigidbody.constraints =
       RigidbodyConstraints.FreezeRotationZ
     | RigidbodyConstraints.FreezeRotationX
     | RigidbodyConstraints.FreezeRotationY
     | RigidbodyConstraints.FreezePositionY;
    }

    private void AKeyIsPressed(KeyAEvent _event)
    {
        if (currentSubState == WallRunStates.WallRunning)
        {
            AKey = CalculateInput(KeyCode.A);
        }
    }
    private void DKeyIsPressed(KeyDEvent _event)
    {
        if (currentSubState == WallRunStates.WallRunning)
        {
            DKey = CalculateInput(KeyCode.D);
        }
    }
    private void SpaceKeyisPressed(KeySpaceEvent _event)
    {
        if (currentSubState == WallRunStates.WallRunning)
        {
            ResetTimer();
            JumpFromWall();
            TurnOnYPos();
            currentSubState = WallRunStates.InAir;
        }
    }



    private void CheckInput()
    {
        //WKey = CalculateInput(KeyCode.W);
        AKey = CalculateInput(KeyCode.A);
        //SKey = CalculateInput(KeyCode.S);
        DKey = CalculateInput(KeyCode.D);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            ResetTimer();
            JumpFromWall();
            TurnOnYPos();
            currentSubState = WallRunStates.InAir;
        }
    }
    private int CalculateInput(KeyCode _keyCode)
    {
        //int value;
        //if (Input.GetKey(_keyCode))
        //{
        //    if (_keyCode == KeyCode.W || _keyCode == KeyCode.D)
        //    {
        //        value = 1;
        //    }
        //    else
        //    {
        //        value = -1;
        //    }
        //}
        //else
        //{
        //    value = 0;
        //}

        int value;

        if (_keyCode == KeyCode.W || _keyCode == KeyCode.D)
        {
            value = 1;
        }
        else
        {
            value = 0;
        }
        return value;

    }
    private void JumpFromWall()
    {
        float zAxe;
        float xAxe;

        xAxe = AKey + DKey;
        zAxe = WKey + SKey;

        if (DKey == 1)
        {
            goingRight = true;
        }
        else
        {
            goingRight = false;
        }
        playerRigidbody.AddRelativeForce(new Vector3(xAxe * horizontalJumpForce, 1 * verticalJumpForce, 1 * ForwardpushForce));
    }
    private void CheckForWalls()
    {
        //Rechts \
        RaycastHit hit;

        if (goingRight == true)
        {

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(playerBody.transform.position, playerBody.transform.TransformDirection(Vector3.right).normalized, out hit, 1.0f))
            {
                Debug.DrawRay(playerBody.transform.position, playerBody.transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WalkAbleWall") /*&& previousState == Player.MoveStates.Running*/)
                {
                    TurnOffYPos();
                    currentSubState = WallRunStates.WallRunning;

                }
            }
        }
        else
        {
            if (Physics.Raycast(playerBody.transform.position, playerBody.transform.TransformDirection(Vector3.left).normalized, out hit, 1.0f))
            {
                Debug.DrawRay(playerBody.transform.position, playerBody.transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WalkAbleWall") /*&& previousState == Player.MoveStates.Running*/)
                {
                    TurnOffYPos();
                    currentSubState = WallRunStates.WallRunning;

                }
            }
        }
    }

    private void ForcePush()
    {
        playerRigidbody.velocity = new Vector3(1, 1, 10);

    }


    //Events
    private void SubscribeEvents()
    {
        EventManager.Subscribe(typeof(KeyWEvent), onKeyW);
        EventManager.Subscribe(typeof(KeyAEvent), onKeyA);
        EventManager.Subscribe(typeof(KeySEvent), onKeyS);
        EventManager.Subscribe(typeof(KeyDEvent), onKeyD);
        EventManager.Subscribe(typeof(KeySpaceEvent), onKeySpace);
    }
    private void UnSubscribeEvents()
    {
        EventManager.Unsubscribe(typeof(KeyWEvent), onKeyW);
        EventManager.Unsubscribe(typeof(KeyAEvent), onKeyA);
        EventManager.Unsubscribe(typeof(KeySEvent), onKeyS);
        EventManager.Unsubscribe(typeof(KeyDEvent), onKeyD);
        EventManager.Unsubscribe(typeof(KeySpaceEvent), onKeySpace);
    }
    private void LinkEvents()
    {
        onKeyA = AKeyIsPressed;
        onKeyD = DKeyIsPressed;
        onKeySpace = SpaceKeyisPressed;
    }

    //Timersd
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


    //Switch To States
    private void SwitchToWalking()
    {
        OwnerStateMachine.SwitchState(typeof(StateWalking));
    }
    private void SwitchToJump()
    {
        OwnerStateMachine.SwitchState(typeof(StateJumping));
    }
}
