using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{


    private ScriptableObject Target;   
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

        //if (Ammo.transform.position == Target.transform.position)
            ScriptableObject.CreateInstance<Minion>().health = 0;
        GetComponent<MinionBehaviour>().minion.health = 0;
       
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

       // if (Ammo.transform.position ==)
        {
            
            ScriptableObject.CreateInstance<Minion>().health = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            Destroy(Ammo, 1.20f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        

    }
}