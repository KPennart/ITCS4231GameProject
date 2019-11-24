using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuePlayer : MonoBehaviour
{
    public Transform target;

    [SerializeField] Transform kitchen;
    [SerializeField] Transform secondBedroomDoorwayExt;
    [SerializeField] Transform secondBedroom;

    [SerializeField] GameObject secondBedroomDoor;

    private Queue<Vector3> locations;
    private Vector3 nextLocation;

    private bool justArrived;

    NavMeshAgent agent;
    private Animator anim;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        locations = new Queue<Vector3>();
        PopulateLocationQueue();
        setNextDestination();
    }

    private void Update()
    {
        IsWalking();

        if (hasGhostArrived() && justArrived)
        {
            justArrived = false;
            destinationActivity();
            Invoke("setNextDestination", 2f);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {

            setNextDestination();
        }
        

    }

    private void setNextDestination()
    {
        nextLocation = locations.Dequeue();
        locations.Enqueue(nextLocation);
        agent.SetDestination(nextLocation);
        justArrived = true;
    }

    private bool hasGhostArrived()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void destinationActivity()
    {
        if (nextLocation == secondBedroomDoorwayExt.position)
        {
            //Debug.Log("123456");
            if (!secondBedroomDoor.GetComponent<DoorController>().isDoorOpen())
            {
                secondBedroomDoor.GetComponent<DoorController>().GhostOpenDoor();
            }
        }
    }

    /// <summary>
    /// Changes the ghost's animations depending on if she's moving
    /// </summary>
    private void IsWalking()
    {
        if (agent.remainingDistance > agent.stoppingDistance && !anim.GetBool("isWalking"))
        {
            anim.SetBool("isWalking", true);
        }
        else if (agent.remainingDistance <= agent.stoppingDistance && anim.GetBool("isWalking"))
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void PopulateLocationQueue()
    {
        locations.Enqueue(kitchen.position);
        locations.Enqueue(secondBedroomDoorwayExt.position);
        locations.Enqueue(secondBedroom.position);
    }
}
