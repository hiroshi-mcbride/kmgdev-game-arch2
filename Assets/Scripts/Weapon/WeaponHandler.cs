using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a reference to the currently equipped weapon and fires it on input
/// </summary>

public class WeaponHandler : IUpdateable
{
    public bool IsActive { get; set; } = true;
    public int Id { get; }

    private List<IWeapon> weapons = new();
    private IWeapon equippedWeapon;
    public WeaponHandler(params WeaponData[] _weaponDataAssets)
    {
        if (EventManager.InvokeCallback(new UpdateableCreatedEvent(this), out int id))
        {
            Id = id;
        }

        foreach (WeaponData asset in _weaponDataAssets)
        {
            weapons.Add(new Weapon(asset));
        }
        EquipWeapon(0);
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
}