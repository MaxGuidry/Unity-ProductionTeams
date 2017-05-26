using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBehaviour : MonoBehaviour {

    private UnityEngine.AI.NavMeshAgent nav;
	// Use this for initialization
    void OnEnable()
    {
        this.gameObject.AddComponent<DestroyForTesting>().time = 10f;
    }
	void Start () {
        
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.Warp(this.transform.position);
        if (this.gameObject.transform.position.z < 0)
        {
            nav.SetDestination(GameObject.FindGameObjectWithTag("LeftBridge").transform.position);
        }
        else
            nav.SetDestination(GameObject.FindGameObjectWithTag("RightBridge").transform.position);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(this.transform.position, nav.destination) < 1)
            nav.SetDestination(new Vector3(-20, 1, 0));
	}
}
