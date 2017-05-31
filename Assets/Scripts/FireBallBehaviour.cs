using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
   
    public GameObject Ammo;
    public GameObject Spawner;
    public int Speed;

    public void Shoot()
    {

        var spawn = (GameObject)Instantiate(Ammo, Spawner.transform.position, Spawner.transform.rotation);
        spawn.GetComponent<Rigidbody>().velocity = this.transform.forward * Speed;
        

        Destroy(spawn, 2.0f);
    }

    private void DamageMinion(Collider other)
    {
        if (other.tag == "Minion")
            GetComponent<MinionBehaviour>().minion.health = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Minion")
            Shoot();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Minion")
            Shoot();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        
    }
}