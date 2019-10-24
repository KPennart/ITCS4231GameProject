using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Fields on the player object that are used in movement speed calculation
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float sprintSpeedBonus;
    [SerializeField] private float crouchSpeedBonus;

    // Instance of the chracter controller
    private CharacterController charController;
    // Instance of the animation controller
    private Animator anim;

    private void Awake()
    {
        // Set the character and animation controllers equal to component's controller/animator
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Calculate movespeed
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        // Value used to determine if the player should move at a non-standard rate
        float moveSpeedMod = MoveSpeedCalculator();

        // Calculate horizontal and vertical movements
        float horizInput = Input.GetAxis(horizontalInputName) * (movementSpeed + moveSpeedMod);
        float vertInput = Input.GetAxis(verticalInputName) * (movementSpeed + moveSpeedMod);

        // Create Vec3's to hold forward and right movement
        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        // Pass the new Vec3's added together to the charController
        charController.SimpleMove(forwardMovement + rightMovement);

    }

    //Calculate the modifier to apply to movespeed depending on if players are sprinting or crouching
    private float MoveSpeedCalculator()
    {
        // If the player is running and moving forward
        if (anim.GetBool("isRunning") && anim.GetBool("isWalkingForward"))
        {
            // Pass back the bonus for sprinting
            return sprintSpeedBonus;
        }
        // If the player is crouching and moving forward
        else if (anim.GetBool("isCrouching"))
        {
            // Pass back the 'bonus' for crouching
            return crouchSpeedBonus;
        }
        // There is no modifier to apply
        else
        {
            // Pass back 0
            return 0;
        }
    }

}