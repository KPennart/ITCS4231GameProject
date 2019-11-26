using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] Transform ghostTransform;
    [SerializeField] int floor;

    private Light lightSource;
    private new AudioSource audio;

    private float minIntensity = 0f;
    private float maxIntensity = 4f;
    private float lastIntensity = 0;

    public int lightSmoothness = 1;

    private bool flickering = false;

    Queue<float> smoothQueue;
    

    void Start ()
    {
        smoothQueue = new Queue<float>(lightSmoothness);

        lightSource = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = ghostTransform.position - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        if (distance < 10f && SameFloor())
        {
            flickering = true;
        }
        else
        {
            flickering = false;
        }

        while (smoothQueue.Count >= lightSmoothness && flickering)
        {
            lastIntensity -= smoothQueue.Dequeue();
        }

        if (flickering)
        {
            if (Random.Range(0, 5) == 0)
            {
                lightSource.intensity = 0;
            }
            else
            {
                float newVal = Random.Range(minIntensity, maxIntensity);
                smoothQueue.Enqueue(newVal);
                lastIntensity += newVal;

                lightSource.intensity = lastIntensity / (float)smoothQueue.Count;
            }
        }
        else
        {
            lightSource.intensity = 2f;
        }
    }

    private bool SameFloor()
    {
        if (System.Math.Floor(ghostTransform.position.y) == 0 && floor == 1)
        {
            return true;
        }
        else if (System.Math.Floor(ghostTransform.position.y) == 6 && floor == 2)
        {
            return true;
        }
        return false;
    }
}
