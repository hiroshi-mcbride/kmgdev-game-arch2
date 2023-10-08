using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class StateJumping : AbstractState
{
    private bool checkIfGrounded;
    private Rigidbody playerRigidbody;
    private GameObject TempPlayer;
    private StateMachine statmachine;
    private int counter = 0;
    private int counterMax = 100;
    private Vector3 beginPos;





    public StateJumping(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
        statmachine = _ownerStateMachine;
        checkIfGrounded = false;
    }

    public override void OnEnter()
    {
        Debug.Log("Switched to Jumping");
        PlayerSetup();
        Jump();

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        Timer();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        //playerRigidbody.AddForce(Vector3.right * 3);

        if (checkIfGrounded == true)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(TempPlayer.transform.position, TempPlayer.transform.TransformDirection(Vector3.down).normalized, out hit, 2.0f))
            {
                Debug.Log("Did Hit");
                if (!DecideIfMoving())
                {
                    checkIfGrounded = false;
                    SwitchToStanding();

                }
                else
                {
                    checkIfGrounded = false;
                    SwitchTowalking();
                }


            }
            else
            {
                Debug.DrawRay(TempPlayer.transform.position, TempPlayer.transform.TransformDirection(Vector3.down) * 1, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }
    public override void OnExit()
    {
        GameObject.Destroy(TempPlayer);
    }
    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * 500.0f);
    }
    private void PlayerSetup()
    {
        TempPlayer = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        TempPlayer.transform.position = new Vector3(2, 1, 4);
        TempPlayer.AddComponent<Rigidbody>();
        playerRigidbody = TempPlayer.GetComponent<Rigidbody>();
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        beginPos = TempPlayer.transform.position;

    }
    private void SwitchToStanding()
    {
        statmachine.SwitchState(typeof(StateStanding));
    }
    private void SwitchTowalking()
    {
        statmachine.SwitchState(typeof(StateWalking));
    }

    private void Timer()
    {

        if (counter <= counterMax)
        {
            counter++;
        }
        else
        {
            ResetTimer();
            checkIfGrounded = true;
        }
    }
    private void ResetTimer()
    {
        counter = 0;
    }

    private bool DecideIfMoving()
    {
        Vector3 currentPos = TempPlayer.transform.position;
        float checkXMovement = currentPos.x - beginPos.x;
        float checkZMovement = currentPos.z - beginPos.z;
        bool tempBool;

        if (checkXMovement == 0)
        {
            tempBool = false;
        }
        else
        {
            tempBool = true;
        }

        //float distance = Vector3.Distance(currentPos, beginPos);

        return tempBool;
    }
}
