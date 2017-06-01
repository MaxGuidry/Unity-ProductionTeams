using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Policy;
using UnityEngine.EventSystems;

public class WizardAIBehaviour : MonoBehaviour
{

    private GameObject targetGameObject;
    [SerializeField]
    private List<GameObject> objectsICareAbout;
    private bool targeting;
    void Start()
    {
        targeting = false;
        objectsICareAbout = new List<GameObject>();
        StartCoroutine(Look());
    }


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * 3f);

    }

    private IEnumerator Look()
    {
        var mask = 1 << 3;
        while (true)
        {
            var hits = Physics.SphereCastAll(new Ray(this.transform.position - this.transform.forward * 2, this.transform.forward), 3, 10).ToList();
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
                MinionThreat mt = new MinionThreat();
                mt.threat =  (int)(1000f * GetThreat(minion, healthAVG, dmgAVG));
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

    [ContextMenu("Sort")]
    void SortByImportance()
    {
        //copy list 2 times one for health and one for damage
        //sort both then calculate average for both
        //find biggest outliers then sort by the outlier value somehow
        var minionThreats = MinionThreat.CalculateThreats(objectsICareAbout);
        objectsICareAbout.Clear();
        minionThreats.Sort((x,y) => y.threat);
        foreach (var minionThreat in minionThreats)
        {
            objectsICareAbout.Add(minionThreat.go);
        }
        //var healthList = new List<GameObject>(objectsICareAbout);
        //var damageList = new List<GameObject>(objectsICareAbout);
        //healthList.Sort((x, y) => (x.GetComponent<MinionBehaviour>().minion.health));
        //float healthAVG = 0f;
        //foreach (var o in healthList)
        //{
        //    healthAVG += o.GetComponent<MinionBehaviour>().minion.health;
        //}
        //healthAVG = healthAVG / healthList.Count;
        //List<float> outlierHealthValues = new List<float>();
        //foreach (var o in healthList)
        //{
        //    outlierHealthValues.Add((o.GetComponent<MinionBehaviour>().minion.health - healthAVG) / healthAVG);
        //}

        //damageList.Sort((x, y) => (x.GetComponent<MinionBehaviour>().minion.damage));
    }
}
