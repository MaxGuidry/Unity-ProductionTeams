using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (this.transform.position.z > 250f)
                this.transform.position += new Vector3(0, 0, -.5f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (this.transform.position.z < 324f)
                this.transform.position += new Vector3(0, 0, .5f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(0, 0, .1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(0, 0, .1f);
        }
    }
}
