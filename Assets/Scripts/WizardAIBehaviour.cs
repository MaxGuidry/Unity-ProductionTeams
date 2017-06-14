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
    private Animator animator;
    [SerializeField]
    private List<GameObject> objectsICareAbout;

    public GameObject targetGameObject;
    private Minion m;
    private bool targeting;
    [HideInInspector]
    public Wizard wizard;
    private GameObject toRemove;




    //The following variables and functions are incase I cant use dylans stuff
    private void Start()
    {
        animator = GetComponent<Animator>();
        wizard = ScriptableObject.CreateInstance<Wizard>();
        attacking = false;
        agent = GetComponent<NavMeshAgent>();
        //agent.isStopped = true;
        targeting = false;
        objectsICareAbout = new List<GameObject>();
        //StartCoroutine(Look());
    }


    // Update is called once per frame
    private void Update()
    {
        //if (objectsICareAbout.Count > 0)
        //{
        //    List<GameObject> real = new List<GameObject>();
        //    foreach (var o in objectsICareAbout)
        //    {
        //        if (o.GetComponent<MinionBehaviour>().minion.minionType == Minion.MinionType.PLAYER)
        //            real.Add(o);
        //    }
        //    objectsICareAbout.Clear();
        //    objectsICareAbout.AddRange(real);
        //    targeting = false;
        //    attacking = false;
        //    animator.SetTrigger("targetdead");
        //    objectsICareAbout.Remove(targetGameObject);
        //    targetGameObject = null;
        //    if(objectsICareAbout.Count>0)
        //        SortByImportance();
        //}
       // m = targetGameObject.GetComponent<MinionBehaviour>().minion;
        animator.SetFloat("speed", agent.velocity.magnitude);
        if (targetGameObject != null)
        {
            this.transform.LookAt(new Vector3(targetGameObject.transform.position.x, 6f, targetGameObject.transform.position.z));
            #region MADEUP_MATH
            // float angle =
            //     Vector3.Angle(
            //         (new Vector3(targetGameObject.transform.position.x, 0, targetGameObject.transform.position.z) -
            //          new Vector3(this.transform.position.x, 0, this.transform.position.z)).normalized,
            //         this.transform.forward);
            // //float actualangle = Vector3.Dot(
            // //    (new Vector3(targetGameObject.transform.position.x, 0, targetGameObject.transform.position.z) -
            // //     new Vector3(this.transform.position.x, 0, this.transform.position.z)).normalized,
            // //    this.transform.forward);
            //Vector3 test =  (new Vector3(targetGameObject.transform.position.x, 0, targetGameObject.transform.position.z) -
            //     new Vector3(this.transform.position.x, 0, this.transform.position.z)).normalized;
            // Debug.Log(this.transform.forward.x + "x");
            // Debug.Log(this.transform.forward.y + "y");
            // Debug.Log(this.transform.forward.z + "z");
            // Debug.Log(test.x);
            // Debug.Log(test.y);
            // Debug.Log(test.z);
            // //Vector3 test = (targetGameObject.transform.position - this.transform.position).normalized;

            // Vector3 right = new Quaternion(0, Mathf.Sin(angle * Mathf.Deg2Rad) / 2f, 0, Mathf.Cos(angle * Mathf.Deg2Rad) / 2f) * this.transform.forward;
            // Debug.Log(right.normalized);
            // Debug.Log(angle);
            // if (angle > 5f)
            // {
            //     Debug.Log(Vector3.Dot(test, right.normalized));
            //     if (Vector3.Dot(test, right.normalized) > .9)
            //         this.transform.rotation = this.transform.rotation *
            //                                   new Quaternion(0, Mathf.Sin(.01f) / 2f, 0,
            //                                       Mathf.Cos(.01f) / 2f);
            //     else
            //         this.transform.rotation = this.transform.rotation *
            //                                   new Quaternion(0, Mathf.Sin(-.01f) / 2f, 0,
            //                                       Mathf.Cos(-.01f) / 2f);
            //     //Debug.Log(Vector3.Angle(
            //     //    (new Vector3(targetGameObject.transform.position.x, 0, targetGameObject.transform.position.z) -
            //     //     new Vector3(this.transform.position.x, 0, this.transform.position.z)).normalized,
            //     //    this.transform.forward));
            // }
            #endregion MADEUP_MATH

            if (targetGameObject.GetComponent<MinionBehaviour>().minion.health <= 0)
            {
                targeting = false;
                attacking = false;
                animator.SetTrigger("targetdead");
                objectsICareAbout.Remove(targetGameObject);
                targetGameObject = null;
                SortByImportance();
                //StartCoroutine(Look());
            }
        }
        if (targetGameObject != null && Vector3.Distance(transform.position, targetGameObject.transform.position) > agent.stoppingDistance && targeting)
        {
            agent.isStopped = false;
            agent.SetDestination(targetGameObject.transform.position);
        }
        if (targeting && targetGameObject != null && !attacking)
        {
            attacking = true;
            animator.SetTrigger("attack");
        }
    }
    private void Patrol()
    {

    }
    private void ShootLeftFireBall()
    {

        GetComponent<MaxFireBall>().ShootLeft(targetGameObject);


    }

    private void ShootRightFireBall()
    {
        GetComponent<MaxFireBall>().ShootRight(targetGameObject);

    }

    //private IEnumerator Look()
    //{
    //    //var mask = 1 << 3;
    //    while (true)
    //    {
    //        var hits = new List<RaycastHit>();
    //        Quaternion originrot = this.transform.rotation;
    //        for (var i = 0f; i < 360; i += 30f)
    //        {
    //            this.transform.rotation = this.transform.rotation * new Quaternion(0,
    //                                          Mathf.Sin(30f / 180f * Mathf.PI) / 2f, 0,
    //                                          Mathf.Cos(30f / 180f * Mathf.PI) / 2f);
    //            hits.AddRange(Physics
    //                .SphereCastAll(
    //                    new Ray(rayOrigin.position - (transform.forward * 30f), transform.forward), 5, 200).ToList());
    //        }

    //        this.transform.rotation = originrot;


    //        foreach (var hit in hits)
    //            if (hit.transform != null)
    //            {
    //                var v = hit.transform.gameObject.GetComponent<MinionBehaviour>();

    //                if (!objectsICareAbout.Contains(hit.transform.gameObject) && v != null && v.minion.minionType == Minion.MinionType.PLAYER)
    //                {
    //                    objectsICareAbout.Add(hit.transform.gameObject);
    //                }
    //            }
    //        SortByImportance();
    //        yield return new WaitForSeconds(.5f);
    //    }
    //}

    private void SortByImportance()
    {
        var minionThreats = MinionThreat.CalculateThreats(objectsICareAbout);
        objectsICareAbout.Clear();
        minionThreats = minionThreats.OrderByDescending(x => x.threat).ToList();
        foreach (var minionThreat in minionThreats)
            objectsICareAbout.Add(minionThreat.go);

        if (!targeting && objectsICareAbout.Count > 0)
        {
            foreach (var o in objectsICareAbout)
            {
                var v = o.GetComponent<MinionBehaviour>().minion;
                if (v.health <= 0)
                    toRemove = o;
            }
            if (toRemove != null)
                objectsICareAbout.Remove(toRemove);
            if (objectsICareAbout.Count > 0)
                Target(objectsICareAbout[0]);
            else
                targetGameObject = null;
        }
    }

    private void Target(GameObject go)
    {
        targetGameObject = go;
     
        targeting = true;
        //agent.isStopped = false;
        agent.SetDestination(go.transform.position);
        agent.stoppingDistance = 15f;

        
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

    void OnTriggerEnter(Collider other)
    {
        
        if (other.transform != null)
        {
            var v = other.transform.gameObject.GetComponent<MinionBehaviour>();
            if(v == null)
                return;
            if (!objectsICareAbout.Contains(other.transform.gameObject) && v.minion != null && v.minion.minionType == Minion.MinionType.PLAYER)
            {
                objectsICareAbout.Add(other.transform.gameObject);
            }
        }
        SortByImportance();
    }
}