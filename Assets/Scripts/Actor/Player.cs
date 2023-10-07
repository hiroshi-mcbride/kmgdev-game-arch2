using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasePhysicsActor, IStateRunner
{
    public Scratchpad ObjectData { get; private set; }
    private StateMachine playerMovementFSM;

    private PlayerData playerData;
    private GameObject playerPrefab;
    private Rigidbody playerRigidbody;

    public Player(/*PlayerData _PlayerDataAssets*/)
    {
        //playerData = _PlayerDataAssets;

        PlayerSetup();
        MakeFSM();

        //ObjectData = new Scratchpad();
        //ObjectData.Write("PlayerDataAssets", _PlayerDataAssets);



    }

    private void MakeFSM()
    {
        playerMovementFSM = new StateMachine();
        playerMovementFSM.AddState(new StateStanding(ObjectData, playerMovementFSM));
        playerMovementFSM.AddState(new StateJumping(ObjectData, playerMovementFSM));
        playerMovementFSM.SwitchState(typeof(StateStanding));


        // fsm.EnterSt
        //InitializeActor();

    }

    private void PlayerSetup()
    {
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.position = new Vector3(2, 1, 0);
        capsule.AddComponent<Rigidbody>();
        playerRigidbody = capsule.GetComponent<Rigidbody>();

        //playerRigidbody.AddForce(Vector3.up * 1000.0f);




        Debug.Log("PALYERiNSTANTIATE");
        //playerPrefab = playerData.PlayerPrefab;
        //GameObject.Instantiate(playerPrefab);

    }
}
