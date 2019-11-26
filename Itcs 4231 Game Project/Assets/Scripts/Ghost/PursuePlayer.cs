using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuePlayer : MonoBehaviour
{
    public Transform target;
    [SerializeField] private GameObject player;

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

    private void Awake()
    {
        justArrived = true;
        playerDetected = false;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        AttatchToNavMesh();

        locations = new Queue<Vector3>();
        PopulateLocationQueue();
        setNextDestination();
    }

    private void Update()
    {
        IsWalking();
        GhostAI();
    }

    private void GhostAI()
    {
        if (IsPlayerInRange())
        {
            agent.SetDestination(target.position);
            playerDetected = true;
        }
        else
        {
            if (playerDetected && hasGhostArrived())
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
    }

    private void setNextDestination()
    {
        lastLocation = nextLocation;
        nextLocation = locations.Dequeue();
        locations.Enqueue(nextLocation);
        agent.SetDestination(nextLocation);
        justArrived = true;
    }

    /// <summary>
    /// Function determines whether or not the ghost is at its destination
    /// </summary>
    /// <returns>
    /// Returns true when the ghost has arrived
    /// </returns>
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

    /// <summary>
    /// If the patrol location has any activity such as opening/closing doors it is done here
    /// </summary>
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

    /// <summary>
    /// Populates the Queue for the route the ghost takes to patrol
    /// </summary>
    private void PopulateLocationQueue()
    {
        locations.Enqueue(kitchen.position);
        locations.Enqueue(secondBedroomDoorwayExt.position);
        locations.Enqueue(secondBedroom.position);
        locations.Enqueue(secondBedroomDoorwayExt.position);
    }

    /// <summary>
    /// Function checks to see if the player is Either in line of sight of the ghost, walking too close to the ghost, or crouched too close to the ghost
    /// </summary>
    /// <returns>
    /// Returns true if the player can be detected
    /// </returns>
    private bool IsPlayerInRange()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        //Debug.Log(direction.magnitude);

        if ((direction.magnitude < 15f && angle < 60))
        {
            RaycastHit hit;

            //Debug.DrawRay(transform.position + transform.up, direction.normalized, Color.red);

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit))
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
        }

        if (Math.Floor(target.position.y) == Math.Floor(transform.position.y))
        {
            if (!PlayerAnimationController.isCrouching && PlayerAnimationController.isWalking && direction.magnitude < 10f)
            {
                return true;
            }
            else if (PlayerAnimationController.isCrouching && PlayerAnimationController.isWalking && direction.magnitude < 2f)
            {
                return true;
            }
            else if (!PlayerAnimationController.isCrouching && !PlayerAnimationController.isWalking && direction.magnitude < 5f)
            {
                return true;
            }
            else if (PlayerAnimationController.isCrouching && !PlayerAnimationController.isWalking && direction.magnitude < 2f)
            {
                return true;
            }
        }

        return false;

    }

    /// <summary>
    /// Makes the ghost resume its patrol after losing the player
    /// </summary>
    private void ResetPath()
    {
        agent.SetDestination(nextLocation);
    }

    private int KeyCount()
    {
        int count = 0;
        for (int i = 0; i < player.GetComponent<PlayerCamera>().keyCollection.Length; i++)
        {
            if (player.GetComponent<PlayerCamera>().keyCollection[i])
            {
                count += 1;
            }
        }

        return count;
    }

    private void AttatchToNavMesh()
    {
        UnityEngine.AI.NavMeshHit closestHit;

        if (UnityEngine.AI.NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, UnityEngine.AI.NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!");
    }
}
