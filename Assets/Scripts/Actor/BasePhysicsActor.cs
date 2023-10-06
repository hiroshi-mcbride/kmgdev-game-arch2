using UnityEngine;

/// <summary>
/// Base type for any actor with physics properties that can run physics logic each FixedUpdate frame
/// </summary>
public abstract class BasePhysicsActor : BaseActor, IFixedUpdateable
{
    public Rigidbody PhysicsBody { get; protected set; }

    public virtual void FixedUpdate(float _fixedDelta) { }

}
