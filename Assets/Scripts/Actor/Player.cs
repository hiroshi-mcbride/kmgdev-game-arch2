using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : BasePhysicsActor, IStateRunner, IUpdateable
{
    public Scratchpad ObjectData { get; private set; }
    public enum MoveStates {Standing, Walking, Running, Jumping, WallRunning}
    private MoveStates previousState; 
    private StateMachine playerMovementFSM;

    private PlayerData playerData;
    private GameObject playerDataPrefab;

    private float rotationSpeed = 2.0f;

    


    public Player(PlayerData _PlayerDataAssets)
    {
        //LockCursur();
        ObjectData = new Scratchpad();
        //ObjectData.Write("PlayerDataAssets", _PlayerDataAssets);

        playerData = _PlayerDataAssets;

        SceneObject = GameObject.Instantiate(playerData.PlayerPrefab);
        playerDataPrefab = SceneObject;

        PhysicsBody = playerDataPrefab.GetComponent<Rigidbody>();
        ObjectData.Write("playerDataPrefab", playerDataPrefab);

        previousState = MoveStates.Standing;
        ObjectData.Write("previousState", previousState); 

        MakeFSM();

        base.InitializeActor();
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

    private void LockCursur()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }







}
