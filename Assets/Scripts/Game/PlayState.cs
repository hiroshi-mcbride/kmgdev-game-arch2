using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Primary gameplay loop
/// </summary>
public class PlayState : AbstractState
{
    private WeaponHandler weaponHandler;
    private Player player;
    private EnemyManager enemyManager = new();
    private Action<GameWinEvent> onGameWinEventHandler;
    private Timer gameTimer;

    public PlayState(Scratchpad _ownerData, StateMachine _ownerStateMachine)
        : base(_ownerData, _ownerStateMachine)
    {
        onGameWinEventHandler = _ => OwnerStateMachine.SwitchState(typeof(WinState));
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        OwnerData.Write("scoreCounter", new ScoreCounter());
        enemyManager.AggregateAll();
        enemyManager.InitializeAll(OwnerData.Read<EnemyData>("enemyData"));
        EventManager.Subscribe(typeof(GameWinEvent), onGameWinEventHandler);
        
        weaponHandler = new WeaponHandler(OwnerData.Read<WeaponData[]>("weaponDataAssets"));
        player = new Player(OwnerData.Read<PlayerData>("PlayerData"));
        
        Action onTimeExpiredEventHandler = () => OwnerStateMachine.SwitchState(typeof(LoseState));
        gameTimer = new Timer(OwnerData.Read<float>("playTime"), onTimeExpiredEventHandler);
        EventManager.Invoke(new GameStartEvent(gameTimer));
    }

    public override void OnUpdate()
    {
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        OwnerData.Write("timeLeft", gameTimer.Stop());
        //player.Destroy();
        EventManager.Unsubscribe(typeof(GameWinEvent), onGameWinEventHandler);
    }
}
