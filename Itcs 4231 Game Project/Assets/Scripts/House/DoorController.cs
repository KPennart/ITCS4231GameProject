using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Door;
    [SerializeField] GameObject DoorCam;

    private bool DoorCamActive = false;
    private bool cooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Player.transform.position);

        if (Input.GetKey(KeyCode.E) && !cooldown && PlayerAnimationController.isCrouching)
        {
            Invoke("ResetCooldown", 0.2f);
            cooldown = true;

            DoorCamActive = !DoorCamActive;
            Player.SetActive(!DoorCamActive);
            DoorCam.SetActive(DoorCamActive);

            if (Player.transform.position[2] > Door.transform.position[2])
            {
                Debug.Log("123");
            }
        }
    }

    void ResetCooldown()
    {
        cooldown = false;
    }
}
