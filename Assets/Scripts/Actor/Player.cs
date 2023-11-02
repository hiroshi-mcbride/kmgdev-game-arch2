using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : BasePhysicsActor, IStateRunner, IUpdateable
{
    public Scratchpad ObjectData { get; private set; }
    public enum MoveStates { Standing, Walking, Running, Jumping, WallRunning }
    private MoveStates previousState;
    private StateMachine playerMovementFSM;

    private PlayerData playerData;
    private GameObject playerDataPrefab;

    // Camera
    private GameObject cameraHolderPrefab;
    private PlayerCam playerCameraScript;
    private MoveCamera moveCameraScript;
    private Transform CameraTransform;

    // new Camera variables
    private NewPlayerCam newPlayerCameraScript;
    private NewMoveCamera newMoveCameraScript;
    private Transform camHolderTrans;

    //player Gameobject
    private GameObject playerOrientation;
    private GameObject playerCameraPos;


    private float rotationSpeed = 2.0f;




    public Player(PlayerData _PlayerDataAssets)
    {
        ObjectData = new Scratchpad();
        playerData = _PlayerDataAssets;

        SetupPlayer();
        CameraAssignMent();
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

    private void CameraAssignMent()
    {
        // getting Camera Data
        //cameraHolderPrefab = GameObject.Instantiate(playerData.CameraHolder);
        cameraHolderPrefab = GameObject.Find("CameraHolder 1");
        camHolderTrans = cameraHolderPrefab.transform;
        CameraTransform = cameraHolderPrefab.GetComponentInChildren<Transform>();

        // gets the child object from the player
        playerOrientation = GameObject.Find("orientation");
        playerCameraPos = GameObject.Find("CameraPos");

        //
        newPlayerCameraScript = new NewPlayerCam(CameraTransform, playerOrientation.transform);
        newMoveCameraScript = new NewMoveCamera();
    }

    private void SetupPlayer()
    {
        SceneObject = GameObject.Instantiate(playerData.PlayerPrefab);
        playerDataPrefab = SceneObject;

        PhysicsBody = playerDataPrefab.GetComponent<Rigidbody>();
        ObjectData.Write("playerDataPrefab", playerDataPrefab);

        previousState = MoveStates.Standing;
        ObjectData.Write("previousState", previousState);
    }

    public override void Update()
    {
        newMoveCameraScript.UpdatingCameraHolderPos(camHolderTrans, playerCameraPos.transform);
        newPlayerCameraScript.UpdatingCamera();
        base.Update();
    }








}
