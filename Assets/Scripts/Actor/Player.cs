using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasePhysicsActor, IStateRunner
{
    public Scratchpad ObjectData { get; private set; }
    private StateMachine fsm;

    public Player(/*PlayerData _playerData*/)
    {
        // Actor = GameObject.Instantiate(...
        // PhysicsBody = ...

        ObjectData = new Scratchpad();
        fsm = new StateMachine();
        //
        //
        //
        // fsm.EnterSt
        //InitializeActor();
    }
}
