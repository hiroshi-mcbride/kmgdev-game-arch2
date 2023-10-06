using UnityEngine;

/// <summary>
/// Base type for any actor with physics properties that can run physics logic each FixedUpdate frame
/// </summary>
public abstract class BasePhysicsActor : BaseActor
{
    protected BasePhysicsActor() : base()
    {
    }

    public Rigidbody PhysicsBody { get; protected set; }
}
