using UnityEngine;
public class Projectile : BasePhysicsActor, IProjectile
{
    
    public Projectile(WeaponData _weaponData)
    {
        Transform mainCamera = Camera.main.transform;
        Actor = GameObject.Instantiate(_weaponData.BulletPrefab);
        Actor.transform.position = mainCamera.position + (mainCamera.forward * 5.0f);
        Actor.transform.rotation = Quaternion.Euler(mainCamera.forward);
        PhysicsBody = Actor.GetComponent<Rigidbody>();
        PhysicsBody.AddForce(Actor.transform.forward * _weaponData.BulletSpeed);
        
    }

    public override void FixedUpdate(float _fixedDelta)
    {
        Collider[] hitColliders = new Collider[8];
        int numColliders = Physics.OverlapSphereNonAlloc(Actor.transform.position, 0.5f, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            
        }

    }

    public void OnHit()
    {
        
    }
}
