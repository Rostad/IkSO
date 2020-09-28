using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{

    private float projectileSpeed;
    private IDamageable shooter;
    private AttributeType attributeType;
    private int damageAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward * projectileSpeed) * Time.deltaTime);
    }


    public void Initialize(Vector3 direction, float projectileSpeed, IDamageable shooter, int damageAmount, AttributeType attributeType)
    {
        this.projectileSpeed = projectileSpeed;
        transform.LookAt(transform.position + direction);
        this.shooter = shooter;
        this.damageAmount = damageAmount;
        this.attributeType = attributeType;
        Destroy(gameObject, 5f);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Damagable"))
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if(hit != shooter)
            {
                hit.DoDamage(damageAmount, attributeType);
                shooter.DamageCallback(damageAmount, attributeType);
            }
        }
        Destroy(gameObject, 0.3f);
        projectileSpeed = 0f;
    }
}
