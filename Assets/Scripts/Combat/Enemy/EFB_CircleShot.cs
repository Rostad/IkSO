using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFB_CircleShot : EnemyFireBehaviour
{

    public int numberOfBulletsPerShot;
    public float rotationAfterShot;


    private float startingAngle = 0;
    protected override void Shoot()
    {

        float angleStep = 360 / numberOfBulletsPerShot;
        float currentAngle = startingAngle;

        for(int i = 0; i<= numberOfBulletsPerShot; i++)
        {
            var direction = Quaternion.AngleAxis(currentAngle, Vector3.up) * transform.forward;
            weapon.Shoot(direction, transform.position, user, 0);
            currentAngle += angleStep;
               
        }

        startingAngle += rotationAfterShot;

       /*for(int i = 0; i <= directions.Length - 1; i++)
        {
            weapon.Shoot(directions[i], transform.position, user, 0);               
        }*/
    }
}
