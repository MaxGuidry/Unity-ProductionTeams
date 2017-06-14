using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    public Animator anim;
    private NavMeshAgent agent;
    [HideInInspector]
    public Wizard wiz;
    private bool attacking = false;
    private bool targeting = false;
    private GameObject target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wiz = ScriptableObject.CreateInstance<Wizard>();
        wiz.damage = 10;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targeting)
        {
            this.transform.LookAt(new Vector3(target.transform.position.x, 6f, target.transform.position.z));

            anim.SetFloat("speed", agent.velocity.magnitude);
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            agent.stoppingDistance = 8;
        }
        if (targeting && Vector3.Distance(this.transform.position, target.transform.position) < 10)
        {
            anim.SetTrigger("attack");
            attacking = true;
        }
        if (attacking)
        {
            if (target.GetComponent<MinionBehaviour>().minion.health <= 0)
            {
                anim.SetTrigger("targetdead");
                targeting = false;
                attacking = false;
            }
        }


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
}
