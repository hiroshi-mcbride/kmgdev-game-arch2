using UnityEngine;

/// <summary>
/// Start of the game, main menu.
/// </summary>
public class BeginState : AbstractState
{
    public BeginState(Scratchpad _ownerData, StateMachine _ownerStateMachine) 
        : base(_ownerData, _ownerStateMachine) { }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OwnerStateMachine.SwitchState(typeof(PlayState));
        }       
    }
}
