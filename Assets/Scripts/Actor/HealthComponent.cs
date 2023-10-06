using System;
using UnityEngine;

public class HealthComponent : IDamageable
{
    public float Health { get; private set; }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
        Debug.Log($"Took {_damage} points of damage, remaining health is {Health}");
    }
}
