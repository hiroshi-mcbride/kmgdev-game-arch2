using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateJumping : AbstractState
{
    public StateJumping(Scratchpad _ownerData, StateMachine _ownerStateMachine)
        : base(_ownerData, _ownerStateMachine) { }
}
