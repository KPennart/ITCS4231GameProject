using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isCrouching_bool;

    private float speed;
    private float walkSpeed = 0.05f;
    private float runSpeed = 0.1f;
    private float crouchSpeed = 0.025f;
    private float rotationSpeed;

    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }

        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotationSpeed;

        transform.Translate(0, 0, z);
        transform.Translate(0, y, 0);

        if (isCrouching_bool)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;

            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if (!isCrouching_bool)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
    }

    private void ToggleCrouch()
    {
        if (isCrouching_bool)
        {
            isCrouching_bool = false;
            anim.SetBool("isCrouching", false);
            col_size.height = 2;
            col_size.center = new Vector3(0, 1, 0);
        }
        else
        {
            isCrouching_bool = true;
            anim.SetBool("isCrouching", true);
            speed = crouchSpeed;
            col_size.height = 1;
            col_size.center = new Vector3(0, 0.5f, 0);
        }
    }



}
