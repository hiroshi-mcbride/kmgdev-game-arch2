using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : BasePhysicsActor, IStateRunner
{
    public Scratchpad ObjectData { get; private set; }
    private StateMachine playerMovementFSM;

    private PlayerData playerData;
    private GameObject playerDataPrefab;


    public Player(PlayerData _PlayerDataAssets)
    {
        ObjectData = new Scratchpad();
        //ObjectData.Write("PlayerDataAssets", _PlayerDataAssets);

        playerData = _PlayerDataAssets;

        SceneObject = GameObject.Instantiate(playerData.PlayerPrefab);
        playerDataPrefab = SceneObject;

        PhysicsBody = playerDataPrefab.GetComponent<Rigidbody>();
        ObjectData.Write("playerDataPrefab", playerDataPrefab);

        MakeFSM();

        InitializeActor();
    }

    private void MakeFSM()
    {
        playerMovementFSM = new StateMachine();
        playerMovementFSM.AddState(new StateStanding(ObjectData, playerMovementFSM));
        playerMovementFSM.AddState(new StateJumping(ObjectData, playerMovementFSM));
        playerMovementFSM.AddState(new StateWalking(ObjectData, playerMovementFSM));
        playerMovementFSM.AddState(new StateRunning(ObjectData, playerMovementFSM));
        playerMovementFSM.AddState(new StateWallRunning(ObjectData, playerMovementFSM));


        playerMovementFSM.SwitchState(typeof(StateStanding));

    }

    //private void PlayerSetup()
    //{
    //    GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
    //    capsule.transform.position = new Vector3(2, 1, 0);
    //    capsule.AddComponent<Rigidbody>();
    //    playerRigidbody = capsule.GetComponent<Rigidbody>();

    //    //playerRigidbody.AddForce(Vector3.up * 1000.0f);




    //    Debug.Log("PALYERiNSTANTIATE");
    //    //playerPrefab = playerData.PlayerPrefab;
    //    //GameObject.Instantiate(playerPrefab);

    //}
}
