using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFireBall : MonoBehaviour
{

    public GameObject Ammo;
    public GameObject LeftHand;
    public GameObject RightHand;
    public int Speed;

    public void ShootLeft(GameObject target)
    {
        var left = (GameObject)Instantiate(Ammo, LeftHand.transform.position, LeftHand.transform.rotation);
        left.GetComponent<Rigidbody>().velocity = (LeftHand.transform.right * -1) * Speed;
        Destroy(left, 2.0f);
        StartCoroutine(left.GetComponent<FireballSeek>().seek(target));
    }

    public void ShootRight(GameObject target)
    {
        var right = (GameObject)Instantiate(Ammo, RightHand.transform.position, RightHand.transform.rotation);
        right.GetComponent<Rigidbody>().velocity = (new Quaternion(0, Mathf.Sin((-45f / 180f) * Mathf.PI) / 2f, 0, Mathf.Cos((-45f / 180f) * Mathf.PI) / 2f) * RightHand.transform.right) * Speed;
        Destroy(right, 2.0f);
        StartCoroutine(right.GetComponent<FireballSeek>().seek(target));
    }
    
}
