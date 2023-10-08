using UnityEngine;

/// <summary>
/// Data object which is used to initialize a new weapon.
/// </summary>

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public GameObject Prefab;
    public Vector3 Position;
    public Vector3 Rotation;
    [Min(1)]public int Ammo;
    [Min(0.01f)]public float FireRate;
    public bool IsAutomatic;

    public ProjectileData Bullet;
}
