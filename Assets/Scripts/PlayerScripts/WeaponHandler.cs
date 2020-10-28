using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponHandler : MonoBehaviour
{

    public int damageLevelMultiplier;

    public Weapon fireWeapon;
    public Weapon radiationWeapon;
    public GameObject weaponOrigin;

    public Weapon currentWeapon;


    private void Start()
    {
        currentWeapon = fireWeapon;
        var p = GetComponent<Player>();
        p.OnShoot += OnShoot;
        p.OnAttributeChange += OnAttributeChange;
        
    }


    public void OnShoot(object sender, Player.OnShootEventArgs e)
    {
        int bonusDamage;
        if(currentWeapon == fireWeapon)
        {
            bonusDamage = damageLevelMultiplier * e.fireLevel;
        } 
        else
        {
            bonusDamage = damageLevelMultiplier * e.nuclearLevel;
        }
        currentWeapon.Shoot(weaponOrigin.transform.forward, weaponOrigin.transform.position, e.shooter, bonusDamage);
    }

    public void OnAttributeChange(object sender, EventArgs e)
    {
        currentWeapon = (currentWeapon == fireWeapon) ? radiationWeapon : fireWeapon;
    }
}
