using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] int keyID;
    [SerializeField] GameObject player;
    [SerializeField] GameObject ghost;
    [SerializeField] GameObject fakeGhost;

    private Renderer rend;
    private BoxCollider col;

    public bool playerInteraction;

    private int keyCount;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<BoxCollider>();

        playerInteraction = false;

        keyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteraction();
    }

    void PlayerInteraction()
    {
        if (playerInteraction)
        {
            GivePlayerKey();
            DisableKey();
            keyCount = CountKeys();
            KeyEvent();
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

    void KeyEvent()
    {
        Debug.Log(keyCount);
        switch (keyCount)
        {
            case 3:
                fakeGhost.SetActive(true);
                break;
            case 4:
                
                ghost.SetActive(true);
                break;
        }
    }

    private int CountKeys()
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
}
