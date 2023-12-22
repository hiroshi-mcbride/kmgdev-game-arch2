using System;
using UnityEngine;
public class Projectile : BasePhysicsActor, IPoolable
{
    public Delegate ReturnToPool { get; set; }
    
    private float damage;
    private float radius;
    private Transform mainCamera;
    private Timer lifeTimer;

    private const int PROJECTILE_LAYER = 31;
    public Projectile()
    {
        SceneObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        PhysicsBody = SceneObject.AddComponent<Rigidbody>();
        base.InitializeActor();

        Func<object> returnToPool = () => ReturnToPool.DynamicInvoke(this);
        lifeTimer = new Timer(5.0f, returnToPool, false);
    }
    
    public void Initialize(ProjectileData _projectileData)
    {
        damage = _projectileData.Damage;
        radius = _projectileData.Radius;
        
        mainCamera = Camera.main.transform;
        SceneObject.transform.position = mainCamera.position + mainCamera.forward * 5.0f;
        SceneObject.transform.rotation = mainCamera.rotation;
        SceneObject.transform.localScale = Vector3.one * radius;
        
        PhysicsBody.useGravity = _projectileData.HasGravity;
        PhysicsBody.velocity = Vector3.zero;
        PhysicsBody.AddForce(SceneObject.transform.forward * _projectileData.Speed);
        
        SceneObject.layer = PROJECTILE_LAYER;
        lifeTimer.Start();
    }

    public override void FixedUpdate()
    {
        Collider[] hitColliders = new Collider[8];
        int numColliders = Physics.OverlapSphereNonAlloc(SceneObject.transform.position, radius, hitColliders, ~(1<<8 | 1 << PROJECTILE_LAYER));
        if (numColliders > 0)
        {
            for (int i = 0; i < numColliders; i++)
            {
                if (ActorDirectory.TryLocate(hitColliders[i].gameObject, out IActor actor))
                {
                    actor.GetComponent<IDamageable>()?.TakeDamage(damage);
                }
            }
            ReturnToPool.DynamicInvoke(this);
            lifeTimer.Stop();
        }
    }

    public void OnEnableObject()
    {
        IsActive = true;
    }

    public void OnDisableObject()
    {
        IsActive = false;
    }
}
