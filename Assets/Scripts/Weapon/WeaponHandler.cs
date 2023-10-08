using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a reference to the currently equipped weapon and fires it on input
/// </summary>

public class WeaponHandler : IUpdateable, IDestroyable
{
    public bool IsActive { get; set; } = true;

    private List<PlayerWeapon> weapons = new();
    private PlayerWeapon equippedWeapon;
    private ObjectPool<Projectile> projectilePool = new();
    
    public WeaponHandler(params WeaponData[] _weaponDataAssets)
    {
        foreach (WeaponData asset in _weaponDataAssets)
        {
            weapons.Add(new PlayerWeapon(asset, projectilePool));
        }
        EquipWeapon(0);
        
        EventManager.Invoke(new UpdateableCreatedEvent(this));
    }


    public void Update()
    {
        if (equippedWeapon == null)
        {
            return;
        }

        if (equippedWeapon.IsAutomatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0))
        {
            equippedWeapon.Fire();
        }
    }

    private void EquipWeapon(int _n)
    {
        equippedWeapon = weapons[_n];
    }

    public void FixedUpdate() { }
    public void Destroy()
    {
        EventManager.Invoke(new UpdateableDestroyedEvent(this));
    }
}