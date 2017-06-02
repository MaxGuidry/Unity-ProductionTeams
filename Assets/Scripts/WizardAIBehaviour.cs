using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Policy;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WizardAIBehaviour : MonoBehaviour
{

    private GameObject targetGameObject;
    [SerializeField]
    private List<GameObject> objectsICareAbout;

    private NavMeshAgent agent;
    private bool targeting;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        targeting = false;
        objectsICareAbout = new List<GameObject>();
        StartCoroutine(Look());
    }


    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator Look()
    {
        var mask = 1 << 3;
        while (true)
        {
            var hits = Physics.SphereCastAll(new Ray(this.transform.position - this.transform.forward * 2, this.transform.forward), 3, 20).ToList();
            foreach (var hit in hits)
            {
                if (hit.transform != null)
                {
                    var v = hit.transform.gameObject.GetComponent<MinionBehaviour>();

                    if (!objectsICareAbout.Contains(hit.transform.gameObject) && v != null)
                        objectsICareAbout.Add(hit.transform.gameObject);
                }
            }
            SortByImportance();
            yield return new WaitForSeconds(1);

        }
    }

    private class MinionThreat : Minion
    {
        public GameObject go;
        public int threat;
        public static List<MinionThreat> CalculateThreats(List<GameObject> minions)
        {
            float healthAVG = 0f;
            foreach (var o in minions)
            {
                healthAVG += o.GetComponent<MinionBehaviour>().minion.health;
            }
            healthAVG = healthAVG / minions.Count;
            float dmgAVG = 0f;
            foreach (var o in minions)
            {
                dmgAVG += o.GetComponent<MinionBehaviour>().minion.damage;
            }
            dmgAVG = dmgAVG / minions.Count;
            List<MinionThreat> minionThreats = new List<MinionThreat>();

            foreach (var minion in minions)
            {
                MinionThreat mt = ScriptableObject.CreateInstance<MinionThreat>();
                mt.threat = (int)(1000f * GetThreat(minion, healthAVG, dmgAVG));
                mt.go = minion;
                minionThreats.Add(mt);
            }
            return minionThreats;
        }

        private static float GetThreat(GameObject minion, float healthavg, float dmgavg)
        {
            Minion min = minion.GetComponent<MinionBehaviour>().minion;
            float healtoutlier = (min.health - healthavg) / healthavg;
            float dmgoutlier = (min.damage - dmgavg) / dmgavg;
            return (healtoutlier + dmgoutlier) / 2f;
        }


    }

    void SortByImportance()
    {
        var minionThreats = MinionThreat.CalculateThreats(objectsICareAbout);
        objectsICareAbout.Clear();
        minionThreats = minionThreats.OrderByDescending((x) => x.threat).ToList();
        foreach (var minionThreat in minionThreats)
        {
            objectsICareAbout.Add(minionThreat.go);
        }
        if (!targeting && objectsICareAbout.Count > 0)
            Target(objectsICareAbout[0]);


    }

    void Target(GameObject go)
    {
        agent.isStopped = false;
        agent.SetDestination(go.transform.position);
        agent.stoppingDistance = 3f;
    }

}
