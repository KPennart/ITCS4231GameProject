using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] Camera doorCam;
    [SerializeField] Transform doorTrans;

    private Animator anim;

    private bool cameraCooldown = false;
    private bool doorCooldown = false;
    private bool doorOpen = false;
    private bool doorCamRotated = false;

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
            ChangeCameraAngle();
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

    private void ChangeCameraAngle()
    {
        Vector3 heading = doorCam.transform.position - playerCam.transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        if (doorTrans.rotation.y == 0.5f)
        {
            if (direction.x > 0.1 && !doorCamRotated)
            {
                //Debug.Log(direction);
                doorCamRotated = true;
                doorCam.transform.Rotate(0f, 180f, 0f);
            }
            else if (direction.x < -0.1 && doorCamRotated)
            {
                doorCamRotated = false;
                doorCam.transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            if (direction.z > 0.1 && !doorCamRotated)
            {
                doorCamRotated = true;
                doorCam.transform.Rotate(0f, 180f, 0f);
            }
            else if (direction.z < -0.1 && doorCamRotated)
            {
                doorCamRotated = false;
                doorCam.transform.Rotate(0f, 180f, 0f);
            }
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
