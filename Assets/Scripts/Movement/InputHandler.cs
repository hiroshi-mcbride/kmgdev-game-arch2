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

    public void Update(float _delta)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnKeyW?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnKeyA?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnKeyS?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnKeyD?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnKeyLeftShift?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpace?.Invoke();
        }

    }

    public void FixedUpdate(float _fixedDelta)
    {
        throw new System.NotImplementedException();
    }
}
