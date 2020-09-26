using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaDisplay : MonoBehaviour
{

    private Text manaText;

    // Start is called before the first frame update
    void Start()
    {
        var mana = GameObject.Find("Player").GetComponent<Mana>();
        mana.OnManaChanged += OnManaChanged;

        manaText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnManaChanged(object Sender, Mana.OnManaChangedEventArgs e)
    {
        manaText.text = "MP " + e.newCurrentManaAmount;
    }
}
