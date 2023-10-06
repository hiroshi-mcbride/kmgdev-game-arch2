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

    public Weapon(WeaponData _weaponData)
    {
        weaponData = _weaponData;
        Actor = GameObject.Instantiate(weaponData.Prefab, Camera.main.transform);
        Actor.transform.localPosition = new Vector3(.8f, -0.7f, 1.7f);
        Actor.transform.localRotation = Quaternion.Euler(.0f, -90.0f, .0f);
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
        else
        {
            
        }
    }

    public override void Update(float _delta)
    {
        fireRateTimer.Update(_delta);
    }
}
