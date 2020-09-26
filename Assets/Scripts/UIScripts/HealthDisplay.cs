using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    private Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        var health = GameObject.Find("Player").GetComponent<Health>();
        health.OnHealthChanged += OnHealthChanged;
        healthText = GetComponent<Text>();
    }



    private void OnHealthChanged(object Sender, Health.OnHealthChangedEventArgs e)
    {
        healthText.text = e.newHealthAmount + " / " + e.maxHealthAmount;
    }
}
