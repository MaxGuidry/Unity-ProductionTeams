using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour {
    //[HideInInspector]
    public Tower tower;
    void Start()
    {
        tower = ScriptableObject.CreateInstance<Tower>();
        tower.health = 1000;
    }
}
