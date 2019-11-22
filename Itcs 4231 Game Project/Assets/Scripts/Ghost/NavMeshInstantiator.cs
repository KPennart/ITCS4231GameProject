using UnityEngine;
using System.Collections;

/// <summary>
/// Used to make sure the GO is on the NavMesh.
/// </summary>
public class NavMeshInstantiator : MonoBehaviour
{
    void Start()
    {
        UnityEngine.AI.NavMeshHit closestHit;

        if (UnityEngine.AI.NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, UnityEngine.AI.NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!");
    }
}
