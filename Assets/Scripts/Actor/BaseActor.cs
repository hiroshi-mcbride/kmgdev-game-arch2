using UnityEngine;

/// <summary>
/// Base type for any object in the scene that can run logic each Update frame
/// </summary>
public abstract class BaseActor : IActor, IUpdateable
{
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

    public virtual void Update(float _delta) { }

    public virtual void Destroy()
    {
        GameObject.Destroy(Actor);
        //invoke event OnDestroyed(this)
    }
}
