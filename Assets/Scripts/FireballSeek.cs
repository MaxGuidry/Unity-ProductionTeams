using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSeek : MonoBehaviour
{

    private bool destroyed = false;
    public IEnumerator seek(GameObject target)
    {

        while (!destroyed)
        {
            if (target == null)
            {
                StopAllCoroutines();

                yield return null;

            }
            if (transform == null)
                yield return null;
            this.GetComponent<Rigidbody>().velocity += (target.transform.position - this.transform.position) * .5f;
            if(this.GetComponent<Rigidbody>().velocity.magnitude > 30f)
            {
                this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * 30;
            }
            yield return new WaitForEndOfFrame();
        }


    }
    void OnDestroy()
    {
        destroyed = true;
        StopAllCoroutines();
        StopCoroutine(seek(null));
    }
}
