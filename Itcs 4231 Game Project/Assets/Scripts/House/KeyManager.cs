using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] int keyID;
    [SerializeField] GameObject player;

    private Renderer rend;
    private BoxCollider col;

    public bool playerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<BoxCollider>();

        playerInteraction = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInteraction)
        {
            GivePlayerKey();
            DisableKey();
        }
    }

    void DisableKey()
    {
        rend.enabled = false;
        col.enabled = false;
        playerInteraction = false;
    }

    void GivePlayerKey()
    {
        player.GetComponent<PlayerCamera>().keyCollection[keyID] = true;
    }
}
