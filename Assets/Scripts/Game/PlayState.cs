using UnityEngine;

/// <summary>
/// The primary gameplay loop is run from PlayState.
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
        ServiceLocator<ObjectPool<Projectile>>.Provide(new ObjectPool<Projectile>());
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
