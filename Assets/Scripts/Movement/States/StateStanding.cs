using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStanding : AbstractState
{
    public StateStanding(Scratchpad _ownerData, StateMachine _ownerStateMachine)
        : base(_ownerData, _ownerStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        //OwnerData.Write("ScoreCounter", new ScoreCounter());
        //weaponHandler = new WeaponHandler(OwnerData.Read<WeaponData[]>("weaponDataAssets"));
        Debug.Log(" Standing");
    }

    public override void Update(float _delta)
    {
        //weaponHandler.Update(_delta);
    }
}
