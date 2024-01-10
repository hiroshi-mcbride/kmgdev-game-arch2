using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class Player : BasePhysicsActor, IStateRunner
{
    public Scratchpad ObjectData { get; private set; }
    public enum MoveStates { Standing, Walking, Running, Jumping, WallRunning }
    private MoveStates previousState;
    private StateMachine playerMovementFSM;

    private PlayerData playerData;
    private GameObject playerDataPrefab;

    // Camera
    private GameObject cameraHolderPrefab;
    private Transform CameraTransform;

    // new Camera variables
    private NewPlayerCam newPlayerCameraScript;
    private NewMoveCamera newMoveCameraScript;
    private Transform camHolderTrans;

    //player Gameobject
    private GameObject playerOrientation;
    private GameObject playerCameraPos;


    private float rotationSpeed = 2.0f;
    private  float moveSpeed = 14.0f;

    // player Input
    private float horizontalInput;
    private float verticalInput;


    private Vector3 moveDirection;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool isActive = true;
    public override bool IsActive
    {
        get => isActive;
        set
        {
            if (isActive != value)
            {
                SceneObject.SetActive(value);
                playerMovementFSM.IsActive = value;
                isActive = value;
            }
        }
    }

    public Player(PlayerData _PlayerDataAssets)
    {
        ObjectData = new Scratchpad();
        playerData = _PlayerDataAssets;

        SetupPlayer();
        CameraAssignMent();
        MakeFSM();

        base.InitializeActor();
    }
    public override void Update()
    {
        newMoveCameraScript.UpdatingCameraHolderPos(camHolderTrans, playerCameraPos.transform);
        newPlayerCameraScript.UpdatingCamera();
        MyInput();
        base.Update();
    }

    public override void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
        base.FixedUpdate();
    }

    public void Reset()
    {
        SceneObject.transform.position = startPosition;
        SceneObject.transform.rotation = startRotation;
        playerOrientation.transform.rotation = startRotation;
        PhysicsBody.velocity = Vector3.zero;
        horizontalInput = 0;
        verticalInput = 0;
        playerMovementFSM.SwitchState(typeof(StateStanding));
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
        startPosition = SceneObject.transform.position;
        startRotation = SceneObject.transform.rotation;

        PhysicsBody = playerDataPrefab.GetComponent<Rigidbody>();
        ObjectData.Write("playerDataPrefab", playerDataPrefab);

        previousState = MoveStates.Standing;
        ObjectData.Write("previousState", previousState);
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //calculate movementDirection
        moveDirection = playerOrientation.transform.forward * verticalInput + playerOrientation.transform.right * horizontalInput;

        PhysicsBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(PhysicsBody.velocity.x, 0f, PhysicsBody.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            PhysicsBody.velocity = new Vector3(limitedVel.x, PhysicsBody.velocity.y, limitedVel.z);
        }
    }

}
