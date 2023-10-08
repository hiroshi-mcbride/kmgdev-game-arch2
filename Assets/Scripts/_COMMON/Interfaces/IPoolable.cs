using System;

public interface IPoolable
{
    //When an instance of IPoolable is created,
    //ReturnToPool is linked to ObjectPool<T>'s ReturnObjectToPool method.
    Delegate ReturnToPool { get; set; }
    void OnEnableObject();
    void OnDisableObject();
}