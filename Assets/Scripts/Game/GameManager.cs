using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Game root. Any UnityEngine callbacks are run through here and delegated to other classes, mainly a State Machine.
/// </summary>
public class GameManager : MonoBehaviour, IStateRunner
{
    public Scratchpad ObjectData { get; private set; }
    public Scratchpad PlayerDataPad { get; private set; }

    [SerializeField] private WeaponData[] weaponDataAssets;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float playTime;

    private InputHandler inputHandler;

    // PlayerData playerData;
    
    private StateMachine fsm;
    private UpdateManager updateManager;

    private void Awake()
    {
        

        updateManager = new UpdateManager();

        ObjectData = new Scratchpad();
        ObjectData.Write("weaponDataAssets", weaponDataAssets);
        ObjectData.Write("enemyData", enemyData);
        ObjectData.Write("PlayerData", playerData);
        ObjectData.Write("playTime", playTime);


        inputHandler = new InputHandler();

        fsm = new StateMachine();
        fsm.AddState(new BeginState(ObjectData, fsm));
        fsm.AddState(new PlayState(ObjectData, fsm));
        fsm.AddState(new WinState(ObjectData, fsm));
        fsm.AddState(new LoseState(ObjectData, fsm));
        fsm.SwitchState(typeof(PlayState));
    }

    private void Update()
    {
        updateManager.UpdateAll();
    }

    private void FixedUpdate()
    {
        updateManager.FixedUpdateAll();
    }

    private void LateUpdate()
    {
        updateManager.ProcessBuffer();
    }
}
