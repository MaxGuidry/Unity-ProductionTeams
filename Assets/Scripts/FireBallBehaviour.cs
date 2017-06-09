using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{

  
    public GameObject Ammo;
    public GameObject LeftHand;
    public GameObject RightHand;
    public int Speed;

    public void ShootLeft()
    {
        var left = (GameObject) Instantiate(Ammo, LeftHand.transform.position, LeftHand.transform.rotation);
        left.GetComponent<Rigidbody>().velocity = LeftHand.transform.right * -1 * Speed;
        Destroy(left, 2.0f);
    }

    public void ShootRight()
    {
        var right = (GameObject)Instantiate(Ammo, RightHand.transform.position, RightHand.transform.rotation);
        right.GetComponent<Rigidbody>().velocity = RightHand.transform.right * Speed;
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
            ShootRight();
            ShootLeft();
            Destroy(Ammo, 1.20f);
        }

    }

}

