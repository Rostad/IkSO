using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{

    public int RPM;
    public int damage;
    public AttributeType attributeType;


    public float Cooldown
    {
        get { return 60f / RPM; }
    }

    public abstract void Shoot(Vector3 direction, Vector3 origin, IDamageable shooter, int bonusDamage);
 
}
