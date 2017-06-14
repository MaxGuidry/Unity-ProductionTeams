using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private bool lockedOn;
    // Use this for initialization
    void Start()
    {
        lockedOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockedOn)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (this.transform.position.z > 250f)
                    this.transform.position += new Vector3(0, 0, -1f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (this.transform.position.z < 324f)
                    this.transform.position += new Vector3(0, 0, 1f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (this.transform.position.x < 370f)
                    this.transform.position += new Vector3(1.5f, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (this.transform.position.x > 125f)
                    this.transform.position += new Vector3(-1.5f, 0, 0);
            }
        }
    }
}
