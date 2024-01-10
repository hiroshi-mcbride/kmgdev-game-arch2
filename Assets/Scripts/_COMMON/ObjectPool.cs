using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{
    private List<T> activePool = new();
    private List<T> inactivePool = new();
    private Action<T> returnToPoolEventHandler;
    private Action<ObjectPoolResetEvent> returnAllObjectsEventHandler;
    
    public ObjectPool()
    {
        returnToPoolEventHandler = ReturnObjectToPool;
        returnAllObjectsEventHandler = ReturnAllObjectsToPool;
        EventManager.Subscribe(typeof(ObjectPoolResetEvent), returnAllObjectsEventHandler);
    }

    public T RequestObject()
    {
        if (inactivePool.Count > 0)
        {
            return ActivateItem(inactivePool[0]);
        }

        return ActivateItem(AddNewItemToPool());
    }
    
    public void ReturnObjectToPool(T _item)
    {
        if (!activePool.Contains(_item))
        {
            return;
        }

        activePool.Remove(_item);
        _item.OnDisableObject();
        inactivePool.Add(_item);
    }

    public void ReturnAllObjectsToPool(ObjectPoolResetEvent _event)
    {
        foreach (T obj in activePool)
        {
            obj.OnDisableObject();
            inactivePool.Add(obj);
        }

        activePool.Clear();
    }

    private T AddNewItemToPool()
    {
        var instance = (T)Activator.CreateInstance(typeof(T));
        instance.ReturnToPool = returnToPoolEventHandler;
        inactivePool.Add(instance);
        return instance;
    }

    private T ActivateItem(T _item)
    {
        _item.OnEnableObject();
        if (inactivePool.Contains(_item))
        {
            inactivePool.Remove(_item);
        }

        activePool.Add(_item);
        return _item;
    }

    ~ObjectPool()
    {
        EventManager.Unsubscribe(typeof(ObjectPoolResetEvent), returnAllObjectsEventHandler);
    }
}