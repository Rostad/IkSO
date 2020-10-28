using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "weapon", menuName ="Weapons/ProjectileWeapon")]
public class ProjectileWeapon : Weapon
{
    public SimpleProjectile projectile;
    public float projectileSpeed;

    public override void Shoot(Vector3 direction, Vector3 origin, IDamageable shooter, int bonusDamage)
    {

        var p = Instantiate(projectile, origin, Quaternion.identity);
        p.Initialize(direction, projectileSpeed, shooter, damage, attributeType);


    }
}
