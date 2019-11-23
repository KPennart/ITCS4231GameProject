using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] Camera doorCam;

    private Animator anim;

    private bool cameraCooldown = false;
    private bool doorCooldown = false;
    private bool doorOpen = false;

    public bool playerInteraction = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCameraState();
        ChangeDoorState();
        //Debug.Log(test);
    }

    public void ChangeDoorState()
    {
        
        if (playerInteraction && !doorCooldown && !PlayerAnimationController.isCrouching)
        {
            playerInteraction = false;
            doorOpen = !doorOpen;
            Invoke("ResetDoorCooldown", 2.0f);
            doorCooldown = true;

            if (doorOpen)
            {
                anim.Play("Door_Open");
            }
            else
            {
                anim.Play("Door_Close");
            }
        }
    }

    private void ChangeCameraState()
    {
        if (playerInteraction && PlayerAnimationController.isCrouching && !cameraCooldown)
        {
            playerInteraction = false;
            playerCam.enabled = !playerCam.enabled;
            doorCam.enabled = !doorCam.enabled;
            Invoke("ResetCameraCooldown", 0.2f);
            cameraCooldown = true;
        }
        else if (!PlayerAnimationController.isCrouching && !cameraCooldown && doorCam.enabled)
        {
            playerCam.enabled = !playerCam.enabled;
            doorCam.enabled = !doorCam.enabled;
            Invoke("ResetCameraCooldown", 0.2f);
            cameraCooldown = true;
        }
    }

    void ResetCameraCooldown()
    {
        cameraCooldown = false;
    }

    void ResetDoorCooldown()
    {
        doorCooldown = false;
    }
}
