using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game root. Any UnityEngine callbacks are run through here and delegated to other classes, mainly a State Machine.
/// </summary>
public class GameManager : MonoBehaviour, IStateRunner
{
    public Scratchpad ObjectData { get; private set; }

    [SerializeField] private WeaponData[] weaponDataAssets;

    // PlayerData playerData;
    
    private StateMachine fsm;
    private UpdateManager updateManager;

    private void Awake()
    {
        ObjectData = new Scratchpad();
        ObjectData.Write("weaponDataAssets", weaponDataAssets);
        updateManager = new UpdateManager();

        fsm = new StateMachine();
        fsm.AddState(new BeginState(ObjectData, fsm));
        fsm.AddState(new PlayState(ObjectData, fsm));
        fsm.AddState(new WinState(ObjectData, fsm));
        fsm.AddState(new LoseState(ObjectData, fsm));
        fsm.SwitchState(typeof(PlayState));
    }

    private void Update()
    {
        updateManager.UpdateAll();
    }

    private void FixedUpdate()
    {
        updateManager.FixedUpdateAll();
    }
}
