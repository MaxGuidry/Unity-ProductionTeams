using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour {

    public UnityEngine.UI.Text time;
    private float prevtime = 0f;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if ((int)prevtime != (int)Time.time)
        {
            time.text = ((int)Time.time).ToString();
            prevtime = Time.time;
        }
    }
}
