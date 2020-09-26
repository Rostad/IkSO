using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionPoints : MonoBehaviour
{

    public event EventHandler<OnActionEnergyChangedEventArgs> OnActionEnergyChanged;

    public int MaxActionEnergy = 300;
    public float ActionEnergyRegen = 10;

    private float currentActionEnergy;

    public int CurrentActionPoints
    {
        get { return (int) currentActionEnergy / 100; }
    }

    public bool CanSpend()
    {
        return CurrentActionPoints > 0;
    }

    private void Start()
    {
        currentActionEnergy = 0;
    }

    public void AddActionEnergy(float amount)
    {
        currentActionEnergy += amount;
        currentActionEnergy = Mathf.Clamp(currentActionEnergy, 0, MaxActionEnergy);
        OnActionEnergyChanged?.Invoke(this, new OnActionEnergyChangedEventArgs { newActionEnergy = (int)(currentActionEnergy - (CurrentActionPoints * 100)), newActionPoints = CurrentActionPoints });
    }

    public void SpendActionEnergy()
    {
        currentActionEnergy -= 100;
        OnActionEnergyChanged?.Invoke(this, new OnActionEnergyChangedEventArgs { newActionEnergy = (int)(currentActionEnergy - (CurrentActionPoints * 100)), newActionPoints = CurrentActionPoints });
    }

    private void Update()
    {
        AddActionEnergy(ActionEnergyRegen * Time.deltaTime);
    }


    

    public class OnActionEnergyChangedEventArgs : EventArgs
    {
        public int newActionPoints;
        public float newActionEnergy;
    }


}
