using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerShoot : MonoBehaviour
{
    public GameObject Plane;
    public GameObject ammo;
    public GameObject spawner;
  
    public int speed;

    public void Shoot()
    {
        this.spawner = (GameObject) Instantiate(this.spawner, this.spawner.transform.position, this.spawner.transform.rotation);
        var spawner = (GameObject)Instantiate(ammo, this.ammo.transform.position, this.ammo.transform.rotation);
        spawner.GetComponent<Rigidbody>().velocity = ammo.transform.forward * speed;
        Destroy(spawner, 2.0f);
        Shoot();
    }

    private void OnTriggerEnter(Collider other, GameObject target)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if (other.tag == "Player")
            Shoot();
        print("Encounterd");

    }

    private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
                Shoot();
            Debug.Log("Encounterd");


        }
    }

