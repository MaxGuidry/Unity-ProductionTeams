using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Minion minion;
    private GameObject PlayerTower;
    private GameObject EnemyTower;
    private Vector3 targetTower;
    Tower twr;
    private UnityEngine.AI.NavMeshAgent nav;
    // Use this for initialization
    void OnEnable()
    {
        //this.gameObject.AddComponent<DestroyForTesting>().time = 10f;
    }
    void Awake()
    {
        minion = ScriptableObject.CreateInstance<Minion>();
        twr = ScriptableObject.CreateInstance<Tower>();
        twr.health = 1000;

    }
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.Warp(this.transform.position);
        PlayerTower = GameObject.FindGameObjectWithTag("Player Tower");
        EnemyTower = GameObject.FindGameObjectWithTag("Enemy Tower");

        if (Vector3.Distance(this.transform.position, PlayerTower.transform.position) < Vector3.Distance(this.transform.position, EnemyTower.transform.position))
        {
            targetTower = EnemyTower.transform.position;
            minion.minionType = Minion.MinionType.PLAYER;
        }
        else
        {
            targetTower = PlayerTower.transform.position;
            minion.minionType = Minion.MinionType.ENEMY;
        }
            if (this.gameObject.transform.position.z < 0)
        {
            nav.SetDestination(GameObject.FindGameObjectWithTag("LeftBridge").transform.position);
        }
        else
            nav.SetDestination(GameObject.FindGameObjectWithTag("RightBridge").transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, nav.destination) < 3)
            nav.SetDestination(targetTower);

        if(Vector3.Distance(this.transform.position,targetTower)<3)
        {
            StartCoroutine(minion.Attack(twr));
        }
    }
}
