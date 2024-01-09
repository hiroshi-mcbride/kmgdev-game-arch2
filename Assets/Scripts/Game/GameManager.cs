using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Game root. Any UnityEngine callbacks are run through here and delegated to other classes, mainly a State Machine.
/// </summary>
public class GameManager : MonoBehaviour, IStateRunner
{
    public Scratchpad ObjectData { get; private set; }

    [SerializeField] private WeaponData[] WeaponDataAssets;
    [SerializeField] private PlayerData PlayerDataAsset;
    [SerializeField] private EnemyData EnemyDataAsset;
    [SerializeField] private float PlayTime;
    [SerializeField] private GameObject BeginContainer;
    [SerializeField] private GameObject PlayContainer;
    [SerializeField] private GameObject WinContainer;
    [SerializeField] private GameObject LoseContainer;
    
    private StateMachine fsm;
    private UpdateManager updateManager;
    private UIManager uiManager;

    private void Awake()
    {
        updateManager = new UpdateManager();

        CanvasItems canvasItems = new()
        {
            Begin = BeginContainer,
            Play = PlayContainer,
            Win = WinContainer,
            Lose = LoseContainer
        };
        uiManager = new UIManager(canvasItems);

        ObjectData = new Scratchpad();
        ObjectData.Write("weaponDataAssets", WeaponDataAssets);
        ObjectData.Write("enemyData", EnemyDataAsset);
        ObjectData.Write("PlayerData", PlayerDataAsset);
        ObjectData.Write("playTime", PlayTime);
        
        fsm = new StateMachine();
        fsm.AddState(new BeginState(ObjectData, fsm));
        fsm.AddState(new PlayState(ObjectData, fsm));
        fsm.AddState(new WinState(ObjectData, fsm));
        fsm.AddState(new LoseState(ObjectData, fsm));
        fsm.SwitchState(typeof(BeginState));
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
