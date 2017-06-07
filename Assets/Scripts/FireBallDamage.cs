using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDamage : MonoBehaviour
{
    public GameObject FireBall;
    public MinionBehaviour min;
    public ScriptableObject Damage;
    private void DamageMinion(Collider other)
    {
        if (FireBall.transform.position == min.transform.position)
            min.minion.damage = 0;
            min.minion.health = 0;
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
