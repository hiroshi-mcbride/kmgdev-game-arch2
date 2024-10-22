﻿using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// State entered if the player clears all enemies from the level
/// </summary>
public class WinState : AbstractState
{
    public WinState(Scratchpad _ownerData, StateMachine _ownerStateMachine) 
        : base(_ownerData, _ownerStateMachine) { }
    
    public override void OnEnter()
    {
        base.OnEnter();
    }
    
    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OwnerStateMachine.SwitchState(typeof(PlayState));
        }       
    }
}
