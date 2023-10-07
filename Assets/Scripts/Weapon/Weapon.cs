using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseActor, IWeapon
{
    public bool IsAutomatic { get; }
    private WeaponData weaponData;
    private int ammo;
    private Timer fireRateTimer;
    private bool canFire = true;

    public Weapon(WeaponData _weaponData) : base()
    {
        weaponData = _weaponData;
        Actor = GameObject.Instantiate(weaponData.Prefab, Camera.main.transform);
        Actor.transform.localPosition = weaponData.Position;
        Actor.transform.localRotation = Quaternion.Euler(weaponData.Rotation);
        ammo = weaponData.Ammo;
        IsAutomatic = weaponData.IsAutomatic;
        Action enableFire = () => canFire = true;
        fireRateTimer = new Timer(1 / weaponData.FireRate, enableFire, false);
    }

    public void Fire()
    {
        if (!canFire)
        {
            return;
        }

        if (ammo > 0)
        {
            Projectile projectile = new(weaponData);
            Debug.Log("Bang!");
            fireRateTimer.Start();
            canFire = false;
            ammo -= 1;
        }
    }
}
