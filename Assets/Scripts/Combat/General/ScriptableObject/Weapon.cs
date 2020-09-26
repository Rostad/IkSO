using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public abstract void Shoot(Vector3 direction, Vector3 origin);
}
