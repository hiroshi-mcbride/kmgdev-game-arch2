using System;

public interface IPoolable
{
    Delegate ReturnToPool { get; set; }
    void OnEnableObject();
    void OnDisableObject();
}