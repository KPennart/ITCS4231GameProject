using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuePlayer : MonoBehaviour
{
    public Transform target;

    [SerializeField] Transform cameraLocation;

    [SerializeField] Transform kitchen;
    [SerializeField] Transform secondBedroomDoorwayExt;
    [SerializeField] Transform secondBedroom;

    [SerializeField] GameObject secondBedroomDoor;

    private Queue<Vector3> locations;
    private Vector3 nextLocation;
    private Vector3 lastLocation;

    private bool justArrived;
    private bool playerDetected;

    NavMeshAgent agent;
    private Animator anim;

    private void Start()
    {
        justArrived = true;
        playerDetected = false;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        locations = new Queue<Vector3>();
        PopulateLocationQueue();
        setNextDestination();
    }

    private void Update()
    {
        IsWalking();
        if (IsPlayerInRange())
        {
            agent.SetDestination(target.position);
            playerDetected = true;
        }
        else
        {
            if (playerDetected)
            {
                ResetPath();
                playerDetected = false;
            }
            else
            {
                if (hasGhostArrived() && justArrived)
                {
                    justArrived = false;
                    destinationActivity();
                    Invoke("setNextDestination", 2f);
                }
            }
        }

        //Vector3 test = new Vector3(0f, 1f, 0f);
    }

    private void setNextDestination()
    {
        lastLocation = nextLocation;
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
            if (lastLocation == secondBedroom.position && secondBedroomDoor.GetComponent<DoorController>().isDoorOpen())
            {
                secondBedroomDoor.GetComponent<DoorController>().GhostOpenDoor();
            }
            if (lastLocation == kitchen.position && !secondBedroomDoor.GetComponent<DoorController>().isDoorOpen())
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
        locations.Enqueue(secondBedroomDoorwayExt.position);
    }

    private bool IsPlayerInRange()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if ((direction.magnitude < 15f && angle < 60))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, .5f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        else if (!PlayerAnimationController.isCrouching && direction.magnitude < 10f)
        {
            return true;
        }
        else if (PlayerAnimationController.isCrouching && direction.magnitude < 2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ResetPath()
    {
        agent.SetDestination(nextLocation);
    }

}
