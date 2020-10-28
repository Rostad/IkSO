using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFireBehaviour : MonoBehaviour
{

    public Weapon weapon;

    private float nextShotTime;

    protected Enemy user;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + weapon.Cooldown;
        }
    }

    private void Start()
    {
        nextShotTime = -1;
        user = GetComponent<Enemy>();
    }

    protected abstract void Shoot();
}
