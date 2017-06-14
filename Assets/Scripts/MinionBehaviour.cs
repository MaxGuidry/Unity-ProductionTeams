using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class MinionBehaviour : MonoBehaviour
{
    //[HideInInspector]
    public Material BlueMaterial;
    public Material RedMaterial;
    public Minion minion;
    public Animator anim;
    private GameObject PlayerTower;
    private GameObject EnemyTower;
    private Vector3 targetTower;
    private bool attacking;
    public Tower twr;
    private UnityEngine.AI.NavMeshAgent nav;

    public Wizard.OnFireBallHit fireballhit = new Wizard.OnFireBallHit();
    // Use this for initialization
    void OnEnable()
    {
        anim = GetComponent<Animator>();
        if (minion == null)
            minion = ScriptableObject.CreateInstance<Minion>();

        attacking = false;
        PlayerTower = GameObject.FindGameObjectWithTag("Player Tower");
        EnemyTower = GameObject.FindGameObjectWithTag("Enemy Tower");

        if (Vector3.Distance(this.transform.position, PlayerTower.transform.position) < Vector3.Distance(this.transform.position, EnemyTower.transform.position))
        {
            targetTower = EnemyTower.transform.position;
            twr = EnemyTower.GetComponent<TowerBehaviour>().tower;

            minion.minionType = Minion.MinionType.PLAYER;
        }
        else
        {
            targetTower = PlayerTower.transform.position;
            twr = PlayerTower.GetComponent<TowerBehaviour>().tower;
            minion.minionType = Minion.MinionType.ENEMY;
        }
        //this.gameObject.AddComponent<DestroyForTesting>().time = 10f;
    }
    void Awake()
    {

        anim = GetComponent<Animator>();
        if (minion == null)
            minion = ScriptableObject.CreateInstance<Minion>();

        attacking = false;
        PlayerTower = GameObject.FindGameObjectWithTag("Player Tower");
        EnemyTower = GameObject.FindGameObjectWithTag("Enemy Tower");

        if (Vector3.Distance(this.transform.position, PlayerTower.transform.position) < Vector3.Distance(this.transform.position, EnemyTower.transform.position))
        {
            targetTower = EnemyTower.transform.position;
            twr = EnemyTower.GetComponent<TowerBehaviour>().tower;

            minion.minionType = Minion.MinionType.PLAYER;
        }
        else
        {
            targetTower = PlayerTower.transform.position;
            twr = PlayerTower.GetComponent<TowerBehaviour>().tower;
            minion.minionType = Minion.MinionType.ENEMY;
        }
    }
    void Start()
    {
        GetComponent<CapsuleCollider>().enabled = true;
        nav = GetComponent<NavMeshAgent>();
        nav.Warp(this.transform.position);
        PlayerTower = GameObject.FindGameObjectWithTag("Player Tower");
        EnemyTower = GameObject.FindGameObjectWithTag("Enemy Tower");

        if (Vector3.Distance(this.transform.position, PlayerTower.transform.position) < Vector3.Distance(this.transform.position, EnemyTower.transform.position))
        {
            targetTower = EnemyTower.transform.position;
            twr = EnemyTower.GetComponent<TowerBehaviour>().tower;

            minion.minionType = Minion.MinionType.PLAYER;
        }
        else
        {
            targetTower = PlayerTower.transform.position;
            twr = PlayerTower.GetComponent<TowerBehaviour>().tower;
            minion.minionType = Minion.MinionType.ENEMY;
        }
        GameObject leftb = GameObject.FindGameObjectWithTag("LeftBridge");
        GameObject rightb = GameObject.FindGameObjectWithTag("RightBridge");
        if (Vector3.Distance(this.transform.position, leftb.transform.position) <
            Vector3.Distance(this.transform.position, rightb.transform.position))
        {
            nav.SetDestination(leftb.transform.position);
        }
        else
            nav.SetDestination(rightb.transform.position);

        switch (minion.minionType)
        {
            case Minion.MinionType.ENEMY:
                {
                    var v = this.gameObject.GetComponentsInChildren<Renderer>().ToList();
                    foreach (var renderer in v)
                    {
                        renderer.material = RedMaterial;
                    }
                    break;
                }

            case Minion.MinionType.PLAYER:
                {
                    var v = this.gameObject.GetComponentsInChildren<Renderer>().ToList();
                    foreach (var renderer in v)
                    {
                        renderer.material = BlueMaterial;
                    }
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {


        //bad fix later
        anim.SetFloat("health", minion.health);
        if (Vector3.Distance(this.transform.position, nav.destination) < 3)
        {
            nav.SetDestination(targetTower);
            nav.stoppingDistance = 15;

        }


        if (Vector3.Distance(this.transform.position, targetTower) < 30 && attacking == false)
        {
            anim.SetTrigger("attack");
            attacking = true;
        }
        if (minion.health <= 0)
        {
            GameObject.Destroy(GetComponent<NavMeshAgent>());
            this.gameObject.AddComponent<DestroyForTesting>().time = 2;
            GameObject.Destroy(GetComponent<MinionBehaviour>());


        }
    }
    public void Attack()
    {
        minion.DoDamage(twr);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyFireball") && minion.minionType == Minion.MinionType.PLAYER)
        {
            GameObject.FindObjectOfType<WizardAIBehaviour>().wizard.DoDamage(minion);
            collision.gameObject.GetComponent<FireballSeek>().StopAllCoroutines();
            Destroy(collision.gameObject, .1f);
        }
        else if (collision.gameObject.CompareTag("PlayerFireball") && minion.minionType == Minion.MinionType.ENEMY)
        {
            GameObject.FindObjectOfType<PlayerController>().wiz.DoDamage(minion);
            collision.gameObject.GetComponent<FireballSeek>().StopAllCoroutines();
            Destroy(collision.gameObject, .1f);
        }
    }
    public class ClickedEvent : UnityEngine.Events.UnityEvent
    {

    }
    public ClickedEvent clicked = new ClickedEvent();
    public void OnMouseDown()
    {
        if (minion.minionType == Minion.MinionType.ENEMY)
            FindObjectOfType<PlayerController>().Target(this.gameObject);
        // clicked.Invoke();
    }
}
