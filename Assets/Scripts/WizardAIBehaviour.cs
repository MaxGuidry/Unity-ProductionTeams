using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WizardAIBehaviour : MonoBehaviour
{
    public Transform rayOrigin;
    private NavMeshAgent agent;
    private bool attacking;

    [SerializeField]
    private List<GameObject> objectsICareAbout;

    public GameObject targetGameObject;
    private bool targeting;

    private Wizard wizard;

    private void Start()
    {
        attacking = false;
        agent = GetComponent<NavMeshAgent>();
        //agent.isStopped = true;
        targeting = false;
        objectsICareAbout = new List<GameObject>();
        StartCoroutine(Look());
    }


    // Update is called once per frame
    private void Update()
    {
        if (targeting && targetGameObject != null && !attacking)
        {
            attacking = true;
            StartCoroutine(Attack());
        }
    }
    private void Patrol()
    {

    }
    private IEnumerator Attack()
    {
        Minion m = targetGameObject.GetComponent<MinionBehaviour>().minion;
        wizard.Attack(m);
        if(targetGameObject.GetComponent<MinionBehaviour>().minion.health <= 0)
        {
            StopCoroutine(Attack());
            StartCoroutine(Look());
        }
        yield return new WaitForSeconds(wizard.attackCooldown);
    }

    private IEnumerator Look()
    {
        //var mask = 1 << 3;
        while (true)
        {
            var hits = new List<RaycastHit>();
            Quaternion originrot = this.transform.rotation;
            for (var i = 0f; i < 360; i += 2f)
            {
                this.transform.rotation = this.transform.rotation * new Quaternion(0,
                                              Mathf.Sin((float)i / 180f * Mathf.PI) / 2f, 0,
                                              Mathf.Cos((float)i / 180f * Mathf.PI) / 2f);
                hits.AddRange(Physics
                    .SphereCastAll(
                        new Ray(rayOrigin.position -(this.gameObject.transform.forward * 2f), this.gameObject.transform.forward), 1, 20).ToList());
            }
            this.transform.rotation = originrot;
            //Physics.SphereCastAll(new Ray(this.transform.position - this.transform.forward * 2, this.transform.forward), 3, 20).ToList();
            //hits.AddRange(Physics
            //    .SphereCastAll(new Ray(this.transform.position - this.transform.right * 2, this.transform.right), 3, 20)
            //    .ToList());
            //hits.AddRange(Physics.SphereCastAll(new Ray(this.transform.position - this.transform.right * -2, this.transform.right * -1), 3, 20).ToList());
            //hits.AddRange(Physics.SphereCastAll(new Ray(this.transform.position, new Quaternion(0, Mathf.Sin((45f / 180f) * Mathf.PI), 0, Mathf.Cos((45f / 180f) * Mathf.PI)) * this.transform.forward), 3, 20).ToList());

            foreach (var hit in hits)
                if (hit.transform != null)
                {   
                    var v = hit.transform.gameObject.GetComponent<MinionBehaviour>();

                    if (!objectsICareAbout.Contains(hit.transform.gameObject) && v != null)
                    {
                        objectsICareAbout.Add(hit.transform.gameObject);
                        Debug.Log(hit.transform.gameObject);
                    }
                }
            SortByImportance();
            yield return new WaitForSeconds(1);
        }
    }

    private void SortByImportance()
    {
        var minionThreats = MinionThreat.CalculateThreats(objectsICareAbout);
        objectsICareAbout.Clear();
        minionThreats = minionThreats.OrderByDescending(x => x.threat).ToList();
        foreach (var minionThreat in minionThreats)
            objectsICareAbout.Add(minionThreat.go);
        if (!targeting && objectsICareAbout.Count > 0)
            Target(objectsICareAbout[0]);
    }

    private void Target(GameObject go)
    {
        targetGameObject = go;
        targeting = true;
        //agent.isStopped = false;
        agent.SetDestination(go.transform.position);
        agent.stoppingDistance = 3f;
        StopCoroutine(Look());
    }

    private class MinionThreat : Minion
    {
        public GameObject go;
        public int threat;

        public static List<MinionThreat> CalculateThreats(List<GameObject> minions)
        {
            var healthAVG = 0f;
            foreach (var o in minions)
                healthAVG += o.GetComponent<MinionBehaviour>().minion.health;
            healthAVG = healthAVG / minions.Count;
            var dmgAVG = 0f;
            foreach (var o in minions)
                dmgAVG += o.GetComponent<MinionBehaviour>().minion.damage;
            dmgAVG = dmgAVG / minions.Count;
            var minionThreats = new List<MinionThreat>();

            foreach (var minion in minions)
            {
                var mt = CreateInstance<MinionThreat>();
                mt.threat = (int)(1000f * GetThreat(minion, healthAVG, dmgAVG));
                mt.go = minion;
                minionThreats.Add(mt);
            }
            return minionThreats;
        }

        private static float GetThreat(GameObject minion, float healthavg, float dmgavg)
        {
            var min = minion.GetComponent<MinionBehaviour>().minion;
            var healtoutlier = (min.health - healthavg) / healthavg;
            var dmgoutlier = (min.damage - dmgavg) / dmgavg;
            return (healtoutlier + dmgoutlier) / 2f;
        }
    }
}