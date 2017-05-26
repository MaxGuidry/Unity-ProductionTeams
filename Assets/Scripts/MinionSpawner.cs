using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{

    public GameObject minion;

    [HideInInspector]
    public bool active;
    [HideInInspector]
    public float SpawnCooldown = 10f;
    void Start()
    {
        Time.timeScale = 30;
        active = true;
        StartCoroutine(spawner());
    }
    IEnumerator spawner()
    {
        while (active)
        {
            GameObject go = Instantiate<GameObject>(minion);
            go.transform.position = this.transform.position;

            if (Time.time < 500)
                SpawnCooldown = ((-.0001f * Time.time * Time.time) + 60f);
            else if(Time.time < 1750)
                SpawnCooldown = ((.00002f * (Time.time - 1800f) * (Time.time - 1800f) + 3f));
            
               
            Debug.Log(SpawnCooldown);
            Debug.Log(Time.time);
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }

}
