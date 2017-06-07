using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    gameObject.transform.rotation = gameObject.transform.rotation * new Quaternion(0, Mathf.Sin(.1f) / 2f, 0, Mathf.Cos(.1f) / 2f);
        
    }
}
