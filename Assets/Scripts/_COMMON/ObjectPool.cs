using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{
    private List<T> activePool = new();
    private List<T> inactivePool = new();

    public T RequestObject()
    {
        if (inactivePool.Count > 0)
        {
            return ActivateItem(inactivePool[0]);
        }

        return ActivateItem(AddNewItemToPool());
    }

    public bool TryGetActiveObjects(out T[] _objects)
    {
        _objects = activePool.ToArray();
        return activePool.Count > 0;
    }
    
    public void ReturnObjectToPool(T _item)
    {
        if (!activePool.Contains(_item))
        {
            return;
        }

        activePool.Remove(_item);
        _item.OnDisableObject();
        _item.IsActive = false;
        inactivePool.Add(_item);
    }
    
    private T AddNewItemToPool()
    {
        var instance = (T)Activator.CreateInstance(typeof(T));
        inactivePool.Add(instance);
        return instance;
    }

    private T ActivateItem(T _item)
    {
        _item.OnEnableObject();
        _item.IsActive = true;
        if (inactivePool.Contains(_item))
        {
            inactivePool.Remove(_item);
        }

        activePool.Add(_item);
        return _item;
    }
}