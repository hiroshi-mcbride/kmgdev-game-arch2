using UnityEngine;

/// <summary>
/// Base type for any object in the scene that can run logic each Update frame
/// </summary>
public abstract class BaseActor : IActor, IUpdateable
{
    public int Id { get; protected set; }
    public GameObject Actor { get; protected set; }

    protected bool isActive;
    public bool IsActive
    {
        get => isActive;
        set
        {
            Actor.SetActive(value);
            isActive = value;
        }
    }


    protected BaseActor()
    {
        if (EventManager.InvokeCallback(new UpdateableCreatedEvent(this), out int id))
        {
            Id = id;
        }
    }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    public virtual void Destroy()
    {
        GameObject.Destroy(Actor);
        //invoke event OnDestroyed(this)
    }

}
