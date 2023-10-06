using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSliding : AbstractState
{
    public StateSliding(Scratchpad _ownerData, StateMachine _ownerStateMachine)
        : base(_ownerData, _ownerStateMachine) { }
}
