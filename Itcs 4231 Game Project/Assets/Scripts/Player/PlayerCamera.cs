using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private string mouseX_str, mouseY_str;
    [SerializeField] private float mouseSensitivity_flt;

    [SerializeField] private Transform playerBody;

    private float xAxisClamp_flt;

    private void Awake()
    {
        LockCursor();
        xAxisClamp_flt = 0.0f;
    }


    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX_flt = Input.GetAxis(mouseX_str) * mouseSensitivity_flt * Time.deltaTime;
        float mouseY_flt = Input.GetAxis(mouseY_str) * mouseSensitivity_flt * Time.deltaTime;

        xAxisClamp_flt += mouseY_flt;

        if(xAxisClamp_flt > 75.0f)
        {
            xAxisClamp_flt = 75.0f;
            mouseY_flt = 0.0f;
            ClampXAxisRotationToValue(295.0f);
        }
        else if (xAxisClamp_flt < -75.0f)
        {
            xAxisClamp_flt = -75.0f;
            mouseY_flt = 0.0f;
            ClampXAxisRotationToValue(75.0f);
        }

        transform.Rotate(Vector3.left * mouseY_flt);
        playerBody.Rotate(Vector3.up * mouseX_flt);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
