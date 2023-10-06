using UnityEngine;

/// <summary>
/// The primary gameplay loop is run from PlayState.
/// </summary>
public class PlayState : AbstractState
{
    private UpdateManager updateManager;
    private WeaponHandler weaponHandler;
    public PlayState(Scratchpad _ownerData, StateMachine _ownerStateMachine) 
        : base(_ownerData, _ownerStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        updateManager = new UpdateManager();
        OwnerData.Write("scoreCounter", new ScoreCounter());
        weaponHandler = new WeaponHandler(OwnerData.Read<WeaponData[]>("weaponDataAssets"));
    }

    public override void Update(float _delta)
    {
        updateManager.UpdateAll(_delta);
    }

    public override void FixedUpdate(float _fixedDelta)
    {
        updateManager.FixedUpdateAll(_fixedDelta);
    }
}
