using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{

    public GameObject minion;

    [HideInInspector]
    public bool active;
    [HideInInspector]
    public float SpawnCooldown = 10f;

    private bool auto;
    void Start()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player Tower").transform.position,
                this.transform.position) < 50)
            auto = false;
        else
        {
            auto = true;
        }
            //Time.timeScale =2;
        active = true;
        if(auto)
            StartCoroutine(spawner());
    }
    IEnumerator spawner()
    {
        while (active)
        {
           
            

            if (Time.time < 200)
            {
            
                Spawn(100,5);
                SpawnCooldown = 20;
            }
            else if (Time.time < 400)
            {
                
                Spawn(125, 7);
                SpawnCooldown = 15;
            }
            else if(Time.time > 400 && Time.time < 430)
            {
                //Harder minions
                
                Spawn(500, 50);

                SpawnCooldown = 45;
            }
            else if(Time.time < 700)
            {
               
                Spawn(200, 20);

                SpawnCooldown = 10;
            }
            //add boss minions 
            else if(Time.time <1200)
            {
                
                Spawn(250, 30);

                SpawnCooldown = 7;
            }
            else
            {
                
                Spawn(300, 40);

                SpawnCooldown = 5;
            }
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }

   
    public void Spawn(int h ,int d)
    {
        GameObject go = Instantiate<GameObject>(minion);
        go.transform.position = this.transform.position;
        Minion min = go.GetComponent<MinionBehaviour>().minion;
        min.health = h;
        min.damage = d;
    }

}
