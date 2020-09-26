using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mana : MonoBehaviour
{
    public event EventHandler<OnManaChangedEventArgs> OnManaChanged;

    public int MaxMana = 100;

    public float manaRegen;

    private float currentMana;

    public float CurrentMana
    {
        get { return currentMana; }
    }

    public bool CanSpend(int amount)
    {
        if(currentMana > amount)
        {
            return true;
        }

        return false;
    }


    public void AddMana(float amount)
    {
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, MaxMana);
        OnManaChanged?.Invoke(this, new OnManaChangedEventArgs { newCurrentManaAmount = (int)currentMana, maxManaAmount = MaxMana });
    }

    public void RemoveMana(int amount)
    {
        if(amount > currentMana)
        {
            throw new ArgumentOutOfRangeException("mana");
        }
        currentMana -= amount;
        OnManaChanged?.Invoke(this, new OnManaChangedEventArgs { newCurrentManaAmount = (int)currentMana, maxManaAmount = MaxMana });
    }

    public void Update()
    {
      AddMana(manaRegen * Time.deltaTime);      
    }

    public class OnManaChangedEventArgs : EventArgs
    {
        public int newCurrentManaAmount;
        public int maxManaAmount;
    }

    private void Start()
    {

        AddMana(MaxMana);

    }

}
