using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDamage : MonoBehaviour
{
    public GameObject FireBall;
    private void DamageMinion(Collider other)
    {
        //if(FireBall.transform.position =)
            GetComponent<MinionBehaviour>().minion.health = 0;
        if (other.tag == "Minion")
            GetComponent<MinionBehaviour>().minion.health = 0;
        else
        {
            if (other.tag == "Fireball")
            {
                GetComponent<MinionBehaviour>().minion.health = 0;
                
            }

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
