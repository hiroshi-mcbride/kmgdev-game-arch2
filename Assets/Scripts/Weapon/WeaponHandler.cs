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

    public int Id { get; private set; }

    private IWeapon equippedWeapon;
    public WeaponHandler(WeaponData[] _weaponDataObjects)
    {
        if (EventManager.InvokeCallback(new UpdateableCreatedEvent(this), out int id))
        {
            Id = id;
        }
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
        //currentWeapon = new Weapon();
    }

    public void FixedUpdate() { }
}