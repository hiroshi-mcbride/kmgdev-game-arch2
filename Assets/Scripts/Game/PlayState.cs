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
    private Action<AllEnemiesKilledEvent> onAllEnemiesKilledEventHandler;
    
    private Timer gameTimer;

    public PlayState(Scratchpad _ownerData, StateMachine _ownerStateMachine)
        : base(_ownerData, _ownerStateMachine)
    {
        onAllEnemiesKilledEventHandler = OnAllEnemiesKilled;
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        OwnerData.Write("scoreCounter", new ScoreCounter());
        enemyManager.AggregateAll();
        enemyManager.InitializeAll(OwnerData.Read<EnemyData>("enemyData"));
        EventManager.Subscribe(typeof(AllEnemiesKilledEvent), onAllEnemiesKilledEventHandler);
        
        weaponHandler = new WeaponHandler(OwnerData.Read<WeaponData[]>("weaponDataAssets"));
        player = new Player(OwnerData.Read<PlayerData>("PlayerData"));

        Action onTimeExpiredEventHandler = OnTimerExpired;
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
        EventManager.Unsubscribe(typeof(AllEnemiesKilledEvent), onAllEnemiesKilledEventHandler);
    }

    private void OnAllEnemiesKilled(AllEnemiesKilledEvent _event)
    {
        EventManager.Invoke(new GameWinEvent(gameTimer.Stop()));
        OwnerStateMachine.SwitchState(typeof(WinState));
    }

    private void OnTimerExpired()
    {
        gameTimer.Stop();
        OwnerStateMachine.SwitchState(typeof(LoseState));
        EventManager.Invoke(new GameLoseEvent());
    }
}
