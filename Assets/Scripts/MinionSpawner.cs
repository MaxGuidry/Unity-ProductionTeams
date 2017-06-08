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
        //Time.timeScale =2;
        active = true;
        StartCoroutine(spawner());
    }
    IEnumerator spawner()
    {
        while (active)
        {
            GameObject go = Instantiate<GameObject>(minion);
            go.transform.position = this.transform.position;
            Minion min = go.GetComponent<MinionBehaviour>().minion;
            

            if (Time.time < 200)
            {
                min.health = 100;
                min.damage = 5;
                SpawnCooldown = 20;
            }
            else if (Time.time < 400)
            {
                min.health = 125;
                min.damage = 7;
                SpawnCooldown = 15;
            }
            else if(Time.time > 400 && Time.time < 430)
            {
                //Harder minions
                min.health = 500;
                min.damage = 50;
                go.transform.localScale = new Vector3(2, 2, 2);
                SpawnCooldown = 45;
            }
            else if(Time.time < 700)
            {
                min.health = 200;
                min.damage = 20;
                SpawnCooldown = 10;
            }
            //add boss minions 
            else if(Time.time <1200)
            {
                min.health = 250;
                min.damage = 30;
                SpawnCooldown = 7;
            }
            else
            {
                min.health = 300;
                min.damage = 40;
                SpawnCooldown = 5;
            }
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }

}
