using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponHandler : MonoBehaviour
{

    public Weapon weapon;
    public GameObject weaponOrigin;


    private void Start()
    {
        GetComponent<Player>().OnShoot += OnShoot;
    }


    public void OnShoot(object sender, EventArgs e)
    {
        weapon.Shoot(weaponOrigin.transform.forward, weaponOrigin.transform.position);
    }
}
