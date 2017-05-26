using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {


        if (Input.GetKey(KeyCode.A))
            this.transform.position += new Vector3(-0.1f, 0, 0);
        if (Input.GetKey(KeyCode.W))
            this.transform.position += new Vector3(0, 0, .1f);
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0, 0, -.1f);
        if (Input.GetKey(KeyCode.D))
            this.transform.position += new Vector3(.1f, 0, 0);
    }
}
