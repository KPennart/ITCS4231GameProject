using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] Transform ghostTransform;
    [SerializeField] int floor;
    [SerializeField] GameObject player;

    private Light lightSource;
    private new AudioSource audio;

    private float minIntensity = 0f;
    private float maxIntensity = 4f;
    private float lastIntensity = 0;

    public int lightSmoothness = 1;

    private bool flickering = false;
    private int keyCount = 0;
    private bool flickerOnPlayer = false;
    private bool lightsOff = false;
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
        Vector3 playerHeading = player.transform.position - transform.position;
        float distance = heading.magnitude;
        float playerDistance = playerHeading.magnitude;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("TESTTEST");
            Debug.Log("Distance: " + playerDistance + "   Key Count " + keyCount);
        }
        
        if (keyCount < 3)
        {
            keyCount = 0;
            for (int i = 0; i < player.GetComponent<PlayerCamera>().keyCollection.Length; i++)
            {
                Debug.Log("Key " + i + ": " + player.GetComponent<PlayerCamera>().keyCollection[i]);
                if (player.GetComponent<PlayerCamera>().keyCollection[i])
                {
                    keyCount += 1;
                }
            }
        }

        if (keyCount < 1 && !lightsOff)
        {
            lightSource.enabled = !lightSource.enabled;
            lightsOff = true;
        }

        else if (lightsOff && keyCount >= 1)
        {
            lightSource.enabled = !lightSource.enabled;
            lightsOff = false;
        }

        if (keyCount == 2)
        {
            flickerOnPlayer = true;
        }
        else
        {
            flickerOnPlayer = false;
        }

        if ((distance < 10f && SameFloor()) || (playerDistance < 10f && flickerOnPlayer))
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
