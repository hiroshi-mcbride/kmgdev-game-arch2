using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRunning : AbstractState
{
    public StateRunning(Scratchpad _ownerData, StateMachine _ownerStateMachine)
        : base(_ownerData, _ownerStateMachine) { }
}
