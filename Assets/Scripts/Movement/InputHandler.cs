using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : IUpdateable
{
    public int Id => throw new System.NotImplementedException();

    public bool IsActive { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public delegate void MovementInput();
    public event MovementInput OnKeyW;
    public event MovementInput OnKeyA;
    public event MovementInput OnKeyS;
    public event MovementInput OnKeyD;
    public event MovementInput OnKeyLeftShift;
    public event MovementInput OnSpace;


    public void Update()
    {
        Debug.Log("test");
    } 

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
