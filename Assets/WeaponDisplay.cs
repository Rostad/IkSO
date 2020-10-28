using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{

    private Image image;

    public Color fireColor;
    public Color nuclearColor;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = fireColor;
        GameObject.Find("Player").GetComponent<Player>().OnAttributeChange += OnAttributeChange;
    }

    
    public void OnAttributeChange(object sender, EventArgs e)
    {
        image.color = (image.color == fireColor) ? nuclearColor : fireColor;
    }
}
