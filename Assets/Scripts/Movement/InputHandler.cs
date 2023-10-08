using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : IUpdateable
{
    public bool IsActive { get; set; } = true;

    public delegate void MovementInput();
    //public event MovementInput OnKeyW;
    //public event MovementInput OnKeyA;
    //public event MovementInput OnKeyS;
    //public event MovementInput OnKeyD;
    //public event MovementInput OnKeyLeftShift;
    //public event MovementInput OnSpace;

    public InputHandler()
    {
        EventManager.Invoke(new UpdateableCreatedEvent(this));
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            EventManager.Invoke(new KeyWEvent());
        }
        if (Input.GetKey(KeyCode.A))
        {
            EventManager.Invoke(new KeyAEvent());
        }
        if (Input.GetKey(KeyCode.S))
        {
            EventManager.Invoke(new KeySEvent());
        }
        if (Input.GetKey(KeyCode.D))
        {
            EventManager.Invoke(new KeyDEvent());
        }
        if ( Input.GetKey(KeyCode.Space))
        {
            EventManager.Invoke(new KeySpaceEvent());
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("InputHandler, pressed LeftShift");
            EventManager.Invoke(new KeyLeftShiftEvent());
        }
    } 

    public void FixedUpdate()
    {

    }
}
