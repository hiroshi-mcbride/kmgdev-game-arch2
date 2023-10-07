using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class StateJumping : AbstractState
{
    private bool IsGrounded;
    private Rigidbody playerRigidbody;
    private GameObject TempPlayer;
    private StateMachine statmachine;



    public StateJumping(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
        statmachine = _ownerStateMachine;
    }

    public override void OnEnter()
    {
        PlayerSetup();
        Jump();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(TempPlayer.transform.position, TempPlayer.transform.TransformDirection(Vector3.down).normalized,  out hit, 2.0f ))
        {
            Debug.Log("Did Hit");
            SwitchToStanding();
        }
        else
        {
            Debug.DrawRay(TempPlayer.transform.position, TempPlayer.transform.TransformDirection(Vector3.down) * 1, Color.white);
            Debug.Log("Did not Hit");
        }
    }
    public override void OnExit()
    {
        GameObject.Destroy(TempPlayer);
    }
    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * 1000.0f);
    }
    private void PlayerSetup()
    {
        TempPlayer = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        TempPlayer.transform.position = new Vector3(2, 1, 3);
        TempPlayer.AddComponent<Rigidbody>();
        playerRigidbody = TempPlayer.GetComponent<Rigidbody>();

    }
    private void SwitchToStanding()
    {
        statmachine.SwitchState(typeof(StateStanding));
    }
}
