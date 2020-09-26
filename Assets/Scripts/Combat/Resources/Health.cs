using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;

    public int maxHealth = 100;

    private int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public bool CanSpend(int amount)
    {
        if(currentHealth > amount)
        {
            return true;
        }

        return false;
    }

    public void AddHealth(int amount) 
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { newHealthAmount = currentHealth, maxHealthAmount = maxHealth });
        
    }

    public void RemoveHealth(int amount) 
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { newHealthAmount = currentHealth, maxHealthAmount = maxHealth });

    }

    public class OnHealthChangedEventArgs : EventArgs
    {
        public int newHealthAmount;
        public int maxHealthAmount;
    }

    private void Start()
    {
        AddHealth(maxHealth);
    }


}
