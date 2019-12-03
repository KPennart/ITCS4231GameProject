using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;
    private Transform transform;

    // Start is called before the first frame update
    void Awake()
    {
        transform = GetComponent<Transform>();
        transform.SetPositionAndRotation(playerTrans.position, playerTrans.rotation);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f);
        Invoke("Deactivate", 2f);
    }

    private void Deactivate()
    {
        Debug.Log("AYYYY");
        Destroy(gameObject);
    }
}
