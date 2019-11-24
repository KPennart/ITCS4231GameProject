using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private string mouseX_str, mouseY_str;
    [SerializeField] private float mouseSensitivity_flt;

    [SerializeField] private Transform playerBody;

    [SerializeField] private Camera playerCam;

    public float interactionDistance = 5f;

    private float xAxisClamp_flt;

    public bool[] keyCollection;

    private void Awake()
    {
        LockCursor();
        xAxisClamp_flt = 0.0f;
    }


    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }


    // Start is called before the first frame update
    void Start()
    {
        keyCollection = new bool[5];
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCam.enabled)
        {
            CameraRotation();
        }

        InteractWithObject();
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

    private void InteractWithObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    hit.transform.root.gameObject.GetComponent<DoorController>().playerInteraction = true;
                }
                else if (hit.collider.CompareTag("InteriorTransition"))
                {
                    SceneManager.LoadScene(2);
                }
                else if (hit.collider.CompareTag("Key"))
                {
                    hit.transform.gameObject.GetComponent<KeyManager>().playerInteraction = true;
                }
                else if (hit.collider.CompareTag("Exit Door") && keyCollection[4])
                {
                    UnlockCursor();
                    SceneManager.LoadScene(0);
                }
                else
                {
                    //Debug.Log("False");
                }
            }

        }
    }
}
