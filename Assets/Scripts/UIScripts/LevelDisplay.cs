using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplay : MonoBehaviour
{

    public TextMeshProUGUI fireLevelText;
    public TextMeshProUGUI nuclearLevelText;

    public Slider fireExperienceSlider;
    public Slider nuclearExperienceSlider;

    public void Start()
    {
        
    }

    public void Awake()
    {
        GameObject.Find("Player").GetComponent<Player>().OnLevelChange += OnLevelChange;
    }



    public void OnLevelChange(object sender, Player.OnLevelChangeEventArgs e)
    {
        if (e.attributeType == AttributeType.Fire)
        {
            fireLevelText.text = e.level.ToString();
            fireExperienceSlider.value = (e.experience % 100) / 100;
            
        }
        else if(e.attributeType == AttributeType.Nuclear)
        {
            nuclearLevelText.text = e.level.ToString();
            nuclearExperienceSlider.value = (e.experience % 100) / 100;
        }
    }
}
