using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Scriptable Objects/Projectile Data", order = 0)]
public class ProjectileData : ScriptableObject
{
    public float Damage;
    public float Speed;
    public float Radius;
    public bool HasGravity;
}
