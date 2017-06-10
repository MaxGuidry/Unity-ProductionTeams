using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSeek : MonoBehaviour {

    public IEnumerator seek(GameObject target)
    {

        while (true)
        {
            this.GetComponent<Rigidbody>().velocity += (target.transform.position - this.transform.position) * .5f;
            if(this.GetComponent<Rigidbody>().velocity.magnitude > 30f)
            {
                this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * 30;
            }
            yield return null;
        }


    }
}
