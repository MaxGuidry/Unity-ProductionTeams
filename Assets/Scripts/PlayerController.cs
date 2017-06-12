using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour {

    private NavMeshAgent agent;

    
    void Start () {
        agent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    public void Target(GameObject go)
    {
        agent.SetDestination(go.transform.position);
    }
}
