using UnityEngine;

/// <summary>
/// Base type for any actor with physics properties that can run physics logic each FixedUpdate frame
/// </summary>
public abstract class BasePhysicsActor : BaseActor
{
    public Rigidbody PhysicsBody { get; protected set; }

    protected override void InitializeActor()
    {
        AddComponent(PhysicsBody);
        base.InitializeActor();
    }
}
