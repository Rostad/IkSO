using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{

    private float projectileSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward * projectileSpeed) * Time.deltaTime);
    }


    public void Initialize(Vector3 direction, float projectileSpeed)
    {
        this.projectileSpeed = projectileSpeed;
        transform.LookAt(transform.position + direction);
        Destroy(gameObject, 5f);
    }
}
