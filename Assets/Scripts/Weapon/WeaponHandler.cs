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
    
    private IWeapon equippedWeapon;
    public WeaponHandler(WeaponData[] _weaponDataObjects)
    {
        foreach (WeaponData weaponData in _weaponDataObjects)
        {
            
        }
    }


    public void Update(float _delta)
    {
        if (equippedWeapon == null)
        {
            return;
        }
        equippedWeapon.Update(_delta);

        if (equippedWeapon.IsAutomatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0))
        {
            equippedWeapon.Fire();
        }
    }

    private void EquipWeapon(int _n)
    {
        //currentWeapon = new Weapon();
    }

}