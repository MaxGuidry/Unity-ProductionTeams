using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBehaviour : MonoBehaviour {
    //[HideInInspector]
    public Tower tower;

    public Slider HealthSlider;
    private int maxhealth;
    void Start()
    {
        tower = ScriptableObject.CreateInstance<Tower>();
        maxhealth = 5000;
        tower.health = 5000;
    }

    void Update()
    {
        HealthSlider.value = ((float)tower.health / (float)maxhealth);
    }
}
