using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    // Use this for initialization
    public GameObject ammo;
    public GameObject spawner;
    public GameObject Player;
    public GameObject Target;
    public int speed;

    void Shoot()
    {
        var spawn = (GameObject)Instantiate(ammo, spawner.transform.position, spawner.transform.rotation);
        var Target = (GameObject) Instantiate(Player, this.Target.transform.position, this.Target.transform.rotation);
        //spawn.GetComponent<Rigidbody>().velocity = ammo.transform.forward * speed;
        Target.GetComponent<Rigidbody>().velocity = Player.transform.position;
        Destroy(spawn, 2.0f);
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Shoot();
    }

}
