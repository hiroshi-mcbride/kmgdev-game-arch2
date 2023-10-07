using UnityEngine;

/// <summary>
/// The primary gameplay loop is run from PlayState.
/// </summary>
public class PlayState : AbstractState
{
    private WeaponHandler weaponHandler;
    public PlayState(Scratchpad _ownerData, StateMachine _ownerStateMachine) 
        : base(_ownerData, _ownerStateMachine) { }

    public override void OnEnter()
    {
        base.OnEnter();
        
        OwnerData.Write("scoreCounter", new ScoreCounter());
        weaponHandler = new WeaponHandler(OwnerData.Read<WeaponData[]>("weaponDataAssets"));
        // player = new Player(OwnerData.Read<PlayerData>("playerData"));
    }

    public override void OnUpdate()
    {
    }

    public override void OnFixedUpdate()
    {

    }
}
