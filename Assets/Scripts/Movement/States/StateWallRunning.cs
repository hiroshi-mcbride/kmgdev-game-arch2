using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        currentSubState = WallRunStates.WallRunning;
        Debug.Log("Current State : Wallrunning");
        PlayerSetup();

    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (currentSubState == WallRunStates.WallRunning)
        {
            CheckInput();
        }
        else
        {
            CheckForWalls();
        }
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
