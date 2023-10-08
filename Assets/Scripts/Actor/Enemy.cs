using UnityEngine;

public class Enemy : BaseActor, IDamageable
{
    public float Health { get; private set; }

    public Enemy(GameObject _instance)
    {
        SceneObject = _instance;
        base.InitializeActor();
    }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
        Debug.Log("damage taken!");
    }
}
