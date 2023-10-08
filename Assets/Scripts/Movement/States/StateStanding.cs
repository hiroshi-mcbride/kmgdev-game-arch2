using System;
using UnityEngine;

public class StateStanding : AbstractState
{
    private StateMachine stateMachine;
    private Scratchpad PlayerData;

    private Player.MoveStates previousState;

    int w, a, s, d;

    private Action<KeyWEvent> onKeyW;
    private Action<KeyAEvent> onKeyA;
    private Action<KeySEvent> onKeyS;
    private Action<KeyDEvent> onKeyD;
    private Action<KeySpaceEvent> onKeySpace;
    private Action<KeyLeftShiftEvent> onLeftShift;



    //public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public StateStanding(Scratchpad _ownerData, StateMachine _ownerStateMachine) : base(_ownerData, _ownerStateMachine)
    {
        stateMachine = _ownerStateMachine;
        PlayerData = _ownerData;


    }
    public override void OnEnter()
    {
        LinkEvents();
        SubscribeEvents();
        Debug.Log("Current State : Standing");
        onLeftShift = EventTest;
        EventManager.Subscribe(typeof(KeyLeftShiftEvent), onLeftShift);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        //CheckInput();
        CheckInputEvent();


    }
    public override void OnExit()
    {
        UnSubscribeEvents();
        LinkEvents();
        OwnerData.Delete("previousState");
        previousState = Player.MoveStates.Standing;
        OwnerData.Write("previousState", previousState);
    }


    private void CheckInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            SwitchToWalking();
        }
        if (Input.GetKey(KeyCode.A))
        {
            SwitchToWalking();
        }
        if (Input.GetKey(KeyCode.S))
        {
            SwitchToWalking();
        }
        if (Input.GetKey(KeyCode.D))
        {
            SwitchToWalking();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SwitchToJumping();
        }
    }

    private void CheckInputEvent()
    {
        if (w == 1 || a == 1 || s == 1 || d == 1)
        {
            SwitchToWalking();
        }
    }
    private void EventTest(KeyLeftShiftEvent _event)
    {
        Debug.Log("het werkt!!");

    }

    //Events
    private void SubscribeEvents()
    {
        EventManager.Subscribe(typeof(KeyWEvent), onKeyW);
        EventManager.Subscribe(typeof(KeyAEvent), onKeyA);
        EventManager.Subscribe(typeof(KeySEvent), onKeyS);
        EventManager.Subscribe(typeof(KeyDEvent), onKeyD);
        EventManager.Subscribe(typeof(KeySpaceEvent), onKeySpace);
    }
    private void UnSubscribeEvents()
    {
        EventManager.Unsubscribe(typeof(KeyWEvent), onKeyW);
        EventManager.Unsubscribe(typeof(KeyAEvent), onKeyA);
        EventManager.Unsubscribe(typeof(KeySEvent), onKeyS);
        EventManager.Unsubscribe(typeof(KeyDEvent), onKeyD);
        EventManager.Unsubscribe(typeof(KeySpaceEvent), onKeySpace);
    }
    private void LinkEvents()
    {
        onKeyW = _event => w = 1;
        onKeyA = _event => a = 1;
        onKeyS = _event => s = 1;
        onKeyD = _event => d = 1;

        onKeySpace = SwitchToJumping;
    }


    //SwitchStates
    private void SwitchToWalking()
    {
        stateMachine.SwitchState(typeof(StateWalking));
    }
    private void SwitchToJumping(KeySpaceEvent _event)
    {
        Console.WriteLine("Switch to Jumping");
        stateMachine.SwitchState(typeof(StateJumping));
    }

}
