using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : BaseActor
{
    public bool IsAutomatic { get; }
    private WeaponData weaponData;
    private int ammo;
    private Timer fireRateTimer;
    private bool canFire = true;
    private ObjectPool<Projectile> projectilePool;

    public PlayerWeapon(WeaponData _weaponData, ObjectPool<Projectile> _projectilePool)
    {
        weaponData = _weaponData;
        SceneObject = GameObject.Instantiate(weaponData.Prefab, Camera.main.transform);
        SceneObject.transform.localPosition = weaponData.Position;
        SceneObject.transform.localRotation = Quaternion.Euler(weaponData.Rotation);
        ammo = weaponData.Ammo;
        IsAutomatic = weaponData.IsAutomatic;
        Action enableFire = () => canFire = true;
        fireRateTimer = new Timer(1 / weaponData.FireRate, enableFire, false);

        projectilePool = _projectilePool;
        base.InitializeActor();
    }

    public void Fire()
    {
        if (!canFire)
        {
            return;
        }

        if (ammo > 0)
        {
            Projectile projectile = projectilePool.RequestObject();
            projectile.Initialize(weaponData.Bullet);
            fireRateTimer.Start();
            canFire = false;
            ammo -= 1;
        }
    }
}
