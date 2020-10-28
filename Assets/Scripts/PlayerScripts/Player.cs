using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour, IDamageable
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    public event EventHandler OnAttributeChange;
    public event EventHandler<OnLevelChangeEventArgs> OnLevelChange;

    public static Player instance;


    private float timeOfNextShot = -1f;
    private WeaponHandler weaponHandler;
    private LevelHandler levelHandler;
    private Health health;
    private AttributeType attributeType;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        levelHandler = new LevelHandler(this);
        attributeType = AttributeType.Fire;
        weaponHandler = GetComponent<WeaponHandler>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > timeOfNextShot)
        {
            timeOfNextShot = Time.time + weaponHandler.currentWeapon.Cooldown;
            OnShoot?.Invoke(this, new OnShootEventArgs { shooter = this, fireLevel = levelHandler.FireLevel, nuclearLevel = levelHandler.NuclearLevel});
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnAttributeChange?.Invoke(this, EventArgs.Empty);
            attributeType = (attributeType == AttributeType.Fire) ? AttributeType.Nuclear : AttributeType.Fire;
        }
    }

    public void DoDamage(int damageAmount, AttributeType attributeType)
    {
        if(attributeType != this.attributeType)
        {
            health.RemoveHealth(damageAmount);
            levelHandler.DecreaseLevel(damageAmount, attributeType);
        }
        else
        {
            levelHandler.IncreaseLevel(damageAmount, attributeType);
        }
    }

    public bool IsPlayer()
    {
        return true;
    }


    private class LevelHandler
    {

        /// <summary>
        /// Internal class to handle a player's attribute levels
        /// A player's level affects the damage dealt by the equipped weapon of that attribute
        /// A level equates to 100 experience. A player's level can not exceed 3 or go below 1. 
        /// When reaching level 3 the player can still gain experience to gain a buffer as not to lose level 3 from one hit
        /// </summary>

        private const float MAX_EXPERIENCE = 299f;

        public float fireExperience;
        public float nuclearExperience;

        public int FireLevel
        {
            get { return 1 + (int)fireExperience / 100; }
        }
        
        public int NuclearLevel
        {
            get { return 1 + (int)nuclearExperience / 100; }
        }

        private Player player;

        public LevelHandler(Player player)
        {
            this.player = player;
            fireExperience = 0;
            nuclearExperience = 0;
            player.OnLevelChange?.Invoke(this, new OnLevelChangeEventArgs { attributeType = AttributeType.Nuclear, experience = nuclearExperience, level = NuclearLevel });
            player.OnLevelChange?.Invoke(this, new OnLevelChangeEventArgs { attributeType = AttributeType.Fire, experience = nuclearExperience, level = NuclearLevel });
        }

        public void IncreaseLevel(int damageDealt, AttributeType damageType)
        {
            if (damageType == AttributeType.Fire)
            {
                fireExperience += damageDealt;
                if (fireExperience > MAX_EXPERIENCE)
                    fireExperience = MAX_EXPERIENCE;
                player.OnLevelChange?.Invoke(this, new OnLevelChangeEventArgs { attributeType = AttributeType.Fire, experience = fireExperience, level = FireLevel });
            }
            else
            {
                nuclearExperience += damageDealt;
                if (nuclearExperience > MAX_EXPERIENCE)
                    nuclearExperience = MAX_EXPERIENCE;
                player.OnLevelChange?.Invoke(this, new OnLevelChangeEventArgs { attributeType = AttributeType.Nuclear, experience = nuclearExperience, level = NuclearLevel });
            }
        }

        public void DecreaseLevel(int damageDealt, AttributeType damageType)
        {
            if (damageType == AttributeType.Fire)
            {
                fireExperience -= damageDealt;
                if (fireExperience < 0)
                    fireExperience = 0;
                player.OnLevelChange?.Invoke(this, new OnLevelChangeEventArgs { attributeType = AttributeType.Fire, experience = fireExperience, level = FireLevel });
            }
            else
            {
                nuclearExperience -= damageDealt;
                if (nuclearExperience < 0)
                    nuclearExperience = 0;
                player.OnLevelChange?.Invoke(this, new OnLevelChangeEventArgs { attributeType = AttributeType.Nuclear, experience = nuclearExperience, level = NuclearLevel });
            }
        }
    }

    public class OnShootEventArgs : EventArgs
    {
        public IDamageable shooter;
        public int fireLevel;
        public int nuclearLevel;
    }

    public class OnLevelChangeEventArgs : EventArgs
    {
        public AttributeType attributeType;
        public float experience;
        public int level;
    }
}
