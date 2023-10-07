using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game root. Any UnityEngine callbacks are run through here and delegated to other classes, mainly a State Machine.
/// </summary>
public class GameManager : MonoBehaviour, IStateRunner
{
    public Scratchpad ObjectData { get; private set; }
    public Scratchpad PlayerDataPad { get; private set; }



    [SerializeField] private WeaponData[] weaponDataAssets;
    [SerializeField] private PlayerData[] playerDataAssets;
    
    private StateMachine fsm;
    private StateMachine playerMovementFSM;

    private void Awake()
    {
        ObjectData = new Scratchpad();
        ObjectData.Write("weaponDataAssets", weaponDataAssets);
        fsm = new StateMachine();
        fsm.AddState(new BeginState(ObjectData, fsm));
        fsm.AddState(new PlayState(ObjectData, fsm));
        fsm.AddState(new WinState(ObjectData, fsm));
        fsm.AddState(new LoseState(ObjectData, fsm));
        fsm.SwitchState(typeof(PlayState));

        //---Nathan--
        PlayerDataPad = new Scratchpad();
        PlayerDataPad.Write("PlayerDataAssets", playerDataAssets);
        playerMovementFSM = new StateMachine();
        playerMovementFSM.AddState(new StateStanding(PlayerDataPad, fsm));
        playerMovementFSM.AddState(new StateWalking(PlayerDataPad, fsm));
        playerMovementFSM.AddState(new StateRunning(PlayerDataPad, fsm));
        playerMovementFSM.AddState(new StateJumping(PlayerDataPad, fsm));
        playerMovementFSM.AddState(new StateWallRunning(PlayerDataPad, fsm));
        playerMovementFSM.SwitchState(typeof(StateStanding));


    }

    private void Update()
    {
        fsm.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        fsm.FixedUpdate(Time.fixedDeltaTime);
    }
}
