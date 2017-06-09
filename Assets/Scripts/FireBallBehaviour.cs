using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{

    public GameObject Target;
    public GameObject Ammo;
    public GameObject Spawner;
    public int Speed;

    public void Shoot()
    {
        var spawn = (GameObject) Instantiate(Ammo, Spawner.transform.position, Spawner.transform.rotation);
        spawn.GetComponent<Rigidbody>().velocity = Spawner.transform.forward * Speed;
        Destroy(spawn, 2.0f);
    }

    public void OnEnter(Collision other)
    {
        
    }
    private void DamageMinion(Collider other)
    {

        
    }

    void Update()
    {
        if (Ammo.transform.position == Target.transform.position)
        {
            
            GetComponent<Minion>().TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            Destroy(Ammo, 1.20f);
        }

    }

}

