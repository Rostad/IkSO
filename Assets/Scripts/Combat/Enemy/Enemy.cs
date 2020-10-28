using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private const int DEFAULT_HEALTH = 50;

    public int maxHealth = DEFAULT_HEALTH;
    public AttributeType attributeType;

    private int health;

    public void DamageCallback(int damageDealt, AttributeType damageType)
    {    }

    public void DoDamage(int damageAmount, AttributeType damageType)
    {
        if (attributeType == damageType)
            return;


        health -= damageAmount;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public bool IsPlayer()
    {
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
