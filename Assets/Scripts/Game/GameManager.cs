using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Game root. Any UnityEngine callbacks are run through here and delegated to other classes, mainly a State Machine.
/// </summary>
public class GameManager : MonoBehaviour, IStateRunner
{
    public StateMachine FSM { get; private set; }
    public Scratchpad ObjectData { get; private set; }

    [SerializeField] private WeaponData[] WeaponDataAssets;
    [SerializeField] private PlayerData PlayerDataAsset;
    [SerializeField] private EnemyData EnemyDataAsset;
    [SerializeField] private float PlayTime;
    [SerializeField] private GameObject BeginContainer;
    [SerializeField] private GameObject PlayContainer;
    [SerializeField] private GameObject WinContainer;
    [SerializeField] private GameObject LoseContainer;
    
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
        
        FSM = new StateMachine();
        FSM.AddState(new BeginState(ObjectData, FSM));
        FSM.AddState(new PlayState(ObjectData, FSM));
        FSM.AddState(new WinState(ObjectData, FSM));
        FSM.AddState(new LoseState(ObjectData, FSM));
        FSM.SwitchState(typeof(BeginState));
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
