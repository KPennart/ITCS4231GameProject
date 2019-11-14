using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    //called when the mouse enters a collider
    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    //called when the mouse exits a collider
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
