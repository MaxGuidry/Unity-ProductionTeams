using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{

  
    public GameObject Ammo;
    public GameObject LeftHand;
    public GameObject RightHand;
    public int Speed;

    public void Shoot()
    {
        var left = (GameObject) Instantiate(Ammo, LeftHand.transform.position, LeftHand.transform.rotation);
        var right = (GameObject)Instantiate(Ammo, RightHand.transform.position, RightHand.transform.rotation);
        left.GetComponent<Rigidbody>().velocity = LeftHand.transform.right * -1 * Speed;
        right.GetComponent<Rigidbody>().velocity = RightHand.transform.right * Speed;
        Destroy(left, 2.0f);
        Destroy(right, 2.0f);
    }

    public void OnEnter(Collision other)
    {
        
    }
    private void DamageMinion(Collider other)
    {

        
    }

    void Update()
    {
    
     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            Destroy(Ammo, 1.20f);
        }

    }

}

