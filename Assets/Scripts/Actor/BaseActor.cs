using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base type for any object in the scene that can run logic each Update frame
/// </summary>
public abstract class BaseActor : IActor, IUpdateable, IDestroyable
{
    public GameObject SceneObject { get; protected set; }
    public Dictionary<Type, object> Components { get; } = new();

    private bool isActive = true;
    public bool IsActive
    {
        get => isActive;
        set
        {
            if (isActive != value)
            {
                SceneObject.SetActive(value);
                isActive = value;
            }
        }
    }

    public void AddComponent(object _component)
    {
        Components.Add(_component.GetType(), _component);
    }

    public T GetComponent<T>()
    {
        Components.TryGetValue(typeof(T), out object component);
        return (T)component;
    }
    
    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    public virtual void Destroy()
    {
        Components.Clear();
        ActorDirectory.Remove(SceneObject);
        GameObject.Destroy(SceneObject);
        EventManager.Invoke(new UpdateableDestroyedEvent(this));
    }
    
    protected virtual void InitializeActor()
    {
        Type[] interfaces = GetType().GetInterfaces();
        foreach (Type i in interfaces)
        {
            Components.Add(i, this);
        }
        
        ActorDirectory.Provide(this);
        EventManager.Invoke(new UpdateableCreatedEvent(this));
    }
}
