using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public Light mainLight;

    private Vector4 colorfull;

    private Vector4 currentColor;

    private bool night;
    // Use this for initialization
    void Start()
    {
        night = false;
        colorfull = new Vector4(1, 225f / 255f, 126f / 255f, 1);
        currentColor = mainLight.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (night)
            currentColor = Vector4.MoveTowards(currentColor, colorfull, Time.deltaTime/50f);
        else
            currentColor = Vector4.MoveTowards(currentColor, new Vector4(0, 0, 0, 1), Time.deltaTime/50f);
        if (currentColor == colorfull)
        {
            night = false;
        }
        else if (currentColor == new Vector4(0,0,0,1))
            night = true;
        mainLight.color = currentColor;
    }
}
