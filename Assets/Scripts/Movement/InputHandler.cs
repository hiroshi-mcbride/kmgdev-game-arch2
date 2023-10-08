using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : IUpdateable
{
    public bool IsActive { get; set; } = true;

    public delegate void MovementInput();
    public event MovementInput OnKeyW;
    public event MovementInput OnKeyA;
    public event MovementInput OnKeyS;
    public event MovementInput OnKeyD;
    public event MovementInput OnKeyLeftShift;
    public event MovementInput OnSpace;

    public InputHandler()
    {
        EventManager.Invoke(new UpdateableCreatedEvent(this));
    }

    public void Update()
    {
        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    EventManager.Invoke(new KeyAEvent());
        //}
    } 

    public void FixedUpdate()
    {

    }
}
