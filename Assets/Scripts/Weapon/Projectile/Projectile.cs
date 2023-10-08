using UnityEngine;
public class Projectile : BasePhysicsActor, IPoolable
{
    
    public Projectile(WeaponData _weaponData)
    {
        Transform mainCamera = Camera.main.transform;
        SceneObject = GameObject.Instantiate(_weaponData.BulletPrefab);
        SceneObject.transform.position = mainCamera.position + (mainCamera.forward * 5.0f);
        SceneObject.transform.rotation = Quaternion.Euler(mainCamera.forward);
        PhysicsBody = SceneObject.GetComponent<Rigidbody>();
        PhysicsBody.AddForce(SceneObject.transform.forward * _weaponData.BulletSpeed);
        InitializeActor();
    }

    public override void FixedUpdate()
    {
        Collider[] hitColliders = new Collider[8];
        int numColliders = Physics.OverlapSphereNonAlloc(SceneObject.transform.position, 0.5f, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            if (ActorLocator.TryLocate(hitColliders[i].gameObject, out IActor actor))
            {
                actor.GetComponent<IDamageable>()?.TakeDamage(1.0f);
            }
            
        }

    }

    public void OnHit()
    {
        
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
