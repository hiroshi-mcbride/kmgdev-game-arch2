using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Primary gameplay loop
/// </summary>
public class PlayState : AbstractState
{
    private WeaponHandler weaponHandler;
    private Player player;
    public PlayState(Scratchpad _ownerData, StateMachine _ownerStateMachine) 
        : base(_ownerData, _ownerStateMachine) { }
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        OwnerData.Write("scoreCounter", new ScoreCounter());
        List<Enemy> enemies = EnemyAggregator.AggregateAll();
        foreach (Enemy enemy in enemies)
        {
            enemy.Initialize(OwnerData.Read<EnemyData>("enemyData"));
        }
        weaponHandler = new WeaponHandler(OwnerData.Read<WeaponData[]>("weaponDataAssets"));
        player = new Player(OwnerData.Read<PlayerData>("PlayerData"));
        //player = new Player();
    }

    public override void OnUpdate()
    {
    }

    public override void OnFixedUpdate()
    {

    }
}
