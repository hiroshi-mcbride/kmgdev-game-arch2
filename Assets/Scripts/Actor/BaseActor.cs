using UnityEngine;

/// <summary>
/// Base type for any object in the scene that can run logic each Update frame
/// </summary>
public abstract class BaseActor : IActor, IUpdateable, IDestroyable
{
    public GameObject SceneObject { get; protected set; }

    private bool isActive;
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
    protected void InitializeActor()
    {
        ActorDirectory.Provide(this);
        EventManager.Invoke(new UpdateableCreatedEvent(this));
    }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    public virtual void Destroy()
    {
        ActorDirectory.Remove(SceneObject);
        GameObject.Destroy(SceneObject);
        EventManager.Invoke(new UpdateableDestroyedEvent(this));
    }

}
