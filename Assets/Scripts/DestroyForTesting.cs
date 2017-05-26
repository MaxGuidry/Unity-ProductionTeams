using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyForTesting : MonoBehaviour {
    public float time;
    private float timer;
	// Use this for initialization
	void Start () {
        timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > time)
            Destroy(this.gameObject);
        timer += Time.deltaTime;
	}
}
