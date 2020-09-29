using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        var health = GameObject.Find("Player").GetComponent<Health>();
        health.OnHealthChanged += OnHealthChanged;
        healthSlider = GetComponent<Slider>();
    }



    private void OnHealthChanged(object Sender, Health.OnHealthChangedEventArgs e)
    {
        healthSlider.value = (float)e.newHealthAmount / e.maxHealthAmount;
    }
}
