using UnityEngine;

/// <summary>
/// Data object which is used to initialize a new weapon.
/// </summary>

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public GameObject Prefab;
    [Min(1)]public int Ammo;
    public float Damage;
    [Min(0.01f)]public float FireRate;
    public bool IsAutomatic;
    
    public GameObject BulletPrefab;
    public float BulletSpeed;
}
