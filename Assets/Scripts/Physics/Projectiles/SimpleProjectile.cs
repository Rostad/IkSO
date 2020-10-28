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
            if(hit.IsPlayer() != shooter.IsPlayer())                   //Check to see if the projectile is colliding with something that is considered an enemy. If hit is a player and shooter is an enemy
            {                                                          //we should do damage and vice versa.
                hit.DoDamage(damageAmount, attributeType);
                Destroy(gameObject, 0.8f);
                projectileSpeed = 0f;
                DisableMeshRendererIfExists();
                StopParticleSystemIfExists();
            }
        }

        if (other.tag.Equals("Obstacle"))
        {
            Destroy(gameObject, 0.8f);
            projectileSpeed = 0f;
            DisableMeshRendererIfExists();
            StopParticleSystemIfExists();
        }
    }

    private void DisableMeshRendererIfExists()
    {
        var meshRenderer = GetComponentInChildren<MeshRenderer>();
        if(meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    private void StopParticleSystemIfExists()
    {
        var particleSystem = GetComponentInChildren<ParticleSystem>();
        if(particleSystem != null)
        {
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
