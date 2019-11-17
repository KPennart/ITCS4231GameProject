using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    public Animator anim;

    private bool DoorOpen = false;
    private bool cooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.E) && !cooldown && !PlayerAnimationController.isCrouching)
        {
            DoorOpen = !DoorOpen;
            Invoke("ResetCooldown", 2.0f);
            cooldown = true;

            if (DoorOpen)
            {
                anim.Play("Door_Open");
            }
            else
            {
                anim.Play("Door_Close");
            }
        }
        
    }

    void ResetCooldown()
    {
        cooldown = false;
    }


}
