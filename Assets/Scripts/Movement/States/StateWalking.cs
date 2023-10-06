using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalking : AbstractState
{
    public StateWalking(Scratchpad _ownerData, StateMachine _ownerStateMachine)
        : base(_ownerData, _ownerStateMachine) { }
}
