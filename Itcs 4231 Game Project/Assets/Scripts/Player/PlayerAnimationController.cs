using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // Declare animator variable
    public Animator anim;
    //declare StaminaBar variables
    private bool isSprinting = StaminaBarController.isSprinting;
    private bool canRun = StaminaBarController.canRun;
    public static bool isCrouching = false;
    public static bool isWalking = false;

    void Start()
    {
        // Create instance of animator variable from component
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        //update isSprinting and canRun from StaminaBarController
        isSprinting = StaminaBarController.isSprinting;
        canRun = StaminaBarController.canRun;
        // If the user is running and can run based off of stamina bar information
        if (isSprinting && canRun && anim.GetBool("isCrouching") == false)
        {
            ChangeAnimationStates(true, true, false, false, false);

            // Tell animator player is running
            anim.SetBool("isRunning", true);
        }
        // If the player ISN'T running
        else
        {
            // If the player hits the crouch key
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                // Toggle whether the player is crouching for the stamina bar
                isCrouching = !isCrouching;
                // Toggle whether the player is crouching in the animator
                anim.SetBool("isCrouching", !anim.GetBool("isCrouching"));
            }

            // Let the animator know the player isn't running
            anim.SetBool("isRunning", false);

            // Tell animator to use the forward animation
            if (Input.GetKey(KeyCode.W))
            {
                isWalking = true;
                ChangeAnimationStates(true, true, false, false, false);
            }
            // Tell animator to use the backward animation
            else if (Input.GetKey(KeyCode.S))
            {
                isWalking = true;
                ChangeAnimationStates(true, false, true, false, false);
            }
            // Tell animator to use the left strafe animation
            else if (Input.GetKey(KeyCode.A))
            {
                isWalking = true;
                ChangeAnimationStates(true, false, false, true, false);
            }
            // Tell animator to use the right strafe animation
            else if (Input.GetKey(KeyCode.D))
            {
                isWalking = true;
                ChangeAnimationStates(true, false, false, false, true);
            }
            // Tell animator to use the idle animation
            else
            {
                isWalking = false;
                ChangeAnimationStates(false, false, false, false, false);
            }
        }

    }

    // Sets each of the following animation booleans to the respective value passed in
    private void ChangeAnimationStates(bool a, bool b, bool c, bool d, bool e)
    {
        anim.SetBool("isWalking", a);
        anim.SetBool("isWalkingForward", b);
        anim.SetBool("isWalkingBackward", c);
        anim.SetBool("strafeLeft", d);
        anim.SetBool("strafeRight", e);
    }

    public bool isPlayerCrouching()
    {
        return isCrouching;
    }
}
