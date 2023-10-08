using UnityEngine;

/// <summary>
/// State entered when the timer runs out before all enemies are cleared
/// </summary>
public class LoseState : AbstractState
{
    public LoseState(Scratchpad _ownerData, StateMachine _ownerStateMachine) 
        : base(_ownerData, _ownerStateMachine) { }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log($"You lost! Score: {OwnerData.Read<ScoreCounter>("scoreCounter").TotalScore}");
    }
}
