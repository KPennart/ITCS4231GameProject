using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuePlayer : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    public Animator anim;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);
        
        if (agent.remainingDistance > agent.stoppingDistance && !anim.GetBool("isWalking"))
        {
            //Debug.Log("true");
            anim.SetBool("isWalking", true);
        }
        else if (agent.remainingDistance <= agent.stoppingDistance && anim.GetBool("isWalking"))
        {
            //agent.isStopped = true;
            //Debug.Log("false");
            anim.SetBool("isWalking", false);
        }
        
    }
}
