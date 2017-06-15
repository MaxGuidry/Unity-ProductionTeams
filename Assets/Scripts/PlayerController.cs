using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator anim;
    private bool attacking;
    [HideInInspector]
    public int crystals;
    public GameObject myTower;
    private GameObject target;
    private bool targeting;


    private float exp;
    private float expToNext;
    private int level;
    [SerializeField]
    private GameObject terrain;

    public Text CrystalText;
    [HideInInspector]
    public Wizard wiz;

    private void Start()
    {
        exp = 0;
        level = 1;
        expToNext = 100 * level;

        crystals = 100;
        agent = GetComponent<NavMeshAgent>();
        wiz = ScriptableObject.CreateInstance<Wizard>();
        wiz.damage = 15;
        anim = GetComponent<Animator>();
    }

    private Vector3 dest;


    // Update is called once per frame
    private void Update()
    {

        if (exp >= expToNext)
        {
            LevelUp();
        }

        if (target != null)
        {
            if (targeting)
            {
                transform.LookAt(new Vector3(target.transform.position.x, 6f, target.transform.position.z));
                if (!attacking)
                    anim.SetFloat("speed", agent.velocity.magnitude);
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 15;
            }
            if (targeting && Vector3.Distance(transform.position, target.transform.position) < 30 && !attacking)
            {
                anim.SetTrigger("attack");
                attacking = true;
            }
            if (attacking)
                if (target.GetComponent<MinionBehaviour>().minion.health <= 0)
                {
                    crystals += target.GetComponent<MinionBehaviour>().minion.damage * 5;
                    anim.SetTrigger("targetdead");
                    targeting = false;
                    attacking = false;
                    target = null;
                }
        }
        else
        {
            anim.SetFloat("speed", agent.velocity.magnitude);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //agent.stoppingDistance = 1;
            var cam = Camera.main;
            //var r = cam.ScreenPointToRay(Input.mousePosition);

            var screenToWorld = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenToWorld, out hit))
            {
                dest = hit.point;
                agent.SetDestination(dest);
            }

            //var angleYZ = Vector3.Angle(r.direction, cam.transform.forward);
            //var angleXZ = Vector3.Angle(r.direction, cam.transform.right);
            //new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z));



            //var x = cam.transform.position.x + (cam.transform.position.y - terrain.transform.position.y - 7.5f) *
            //        Mathf.Tan(angleXZ * Mathf.Deg2Rad);

            //var z = cam.transform.position.z - (cam.transform.position.y - terrain.transform.position.y - 7.5f) /
            //        Mathf.Tan(angleYZ * Mathf.Deg2Rad);

            //agent.SetDestination(new Vector3(x, terrain.transform.position.y + 7.5f, z));

            //this.transform.LookAt(agent.destination);
            transform.LookAt(dest);



        }
        CrystalText.text = crystals.ToString() + " Crystals left";

    }

    private void ShootLeftFireBall()
    {
        GetComponent<MaxFireBall>().ShootLeft(target);
    }

    private void ShootRightFireBall()
    {
        GetComponent<MaxFireBall>().ShootRight(target);
    }

    public void Target(GameObject go)
    {
        target = go;
        agent.SetDestination(go.transform.position);
        targeting = true;
    }

    public void SpawnMinion()
    {
        if (crystals >= 50)
        {
            var spawners = myTower.GetComponentsInChildren<MinionSpawner>().ToList();

            if (Time.time < 200)
            {
                spawners[0].Spawn(Random.Range(90, 116), Random.Range(4, 7));
                spawners[1].Spawn(Random.Range(90, 116), Random.Range(4, 7));
            }
            else if (Time.time < 400)
            {
                spawners[0].Spawn(Random.Range(118, 131), Random.Range(5, 11));
                spawners[1].Spawn(Random.Range(118, 131), Random.Range(5, 11));
            }
            else if (Time.time > 400 && Time.time < 430)
            {
                //Harder minions

                spawners[0].Spawn(Random.Range(500, 751), Random.Range(50, 76));
                spawners[1].Spawn(Random.Range(500, 751), Random.Range(50, 76));
            }
            else if (Time.time < 700)
            {
                spawners[0].Spawn(Random.Range(190, 216), Random.Range(17, 26));
                spawners[1].Spawn(Random.Range(190, 216), Random.Range(17, 26));
            }
            //add boss minions 
            else if (Time.time < 1200)
            {
                spawners[0].Spawn(Random.Range(240, 276), Random.Range(24, 41));
                spawners[1].Spawn(Random.Range(240, 276), Random.Range(24, 41));
            }
            else
            {
                spawners[0].Spawn(Random.Range(280, 351), Random.Range(35, 51));
                spawners[1].Spawn(Random.Range(280, 351), Random.Range(35, 51));
            }
            crystals -= 50;
        }
    }

    public void LevelUp()
    {
        level++;
        exp = 0;
        expToNext = 100 * level;
    }
}