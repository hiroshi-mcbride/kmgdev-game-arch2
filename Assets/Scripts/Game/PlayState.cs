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
        Action onTimeExpiredEventHandler = OnTimerExpired;
        gameTimer = new Timer(OwnerData.Read<float>("playTime"), onTimeExpiredEventHandler, false);
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemyManager.InitializeAll(OwnerData.Read<EnemyData>("enemyData"));
        EventManager.Subscribe(typeof(AllEnemiesKilledEvent), onAllEnemiesKilledEventHandler);

        // Today I learned about null-coalescing operators and I love them
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
        
        weaponHandler ??= new WeaponHandler(OwnerData.Read<WeaponData[]>("weaponDataAssets"));
        weaponHandler.IsActive = true;
        
        player ??= new Player(OwnerData.Read<PlayerData>("PlayerData"));
        player.IsActive = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        
        gameTimer.Start();
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
        Cursor.lockState = CursorLockMode.None;
        
        player.Reset();
        player.IsActive = false;
        weaponHandler.IsActive = false;
        EventManager.Invoke(new ObjectPoolResetEvent());
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
