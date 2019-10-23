using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;
    private new AudioSource audio;
    private RawImage flashlightHUD;

    [SerializeField] private AudioClip clickOn;
    [SerializeField] private AudioClip clickOff;

    private float flashlightBattery = 100f;
    private float flashlightRechargeRate = 0.2f;
    private float flashlightDrainRate = 0.1f;

    private void Start()
    {
        flashlight = GetComponent<Light>();
        audio = GetComponent<AudioSource>();
        flashlightHUD = GameObject.Find("Flashlight HUD").GetComponent<RawImage>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            flashlightHUD.enabled = flashlight.enabled;

            audio.clip = audio.clip == clickOn ? clickOff : clickOn;
            audio.Play();
        }


        CheckForDeadBattery();

        
    }

    private void FixedUpdate()
    {
        DrainBattery();
    }

    void DrainBattery()
    {
        if (flashlight.enabled)
        {
            flashlightBattery = System.Math.Max(flashlightBattery - flashlightDrainRate, 0);
        }
        else
        {
            flashlightBattery = System.Math.Min(flashlightBattery + flashlightRechargeRate, 100);
        }

        Debug.Log(flashlightBattery);
    }

    void CheckForDeadBattery()
    {
        if (flashlightBattery == 0 && flashlight.enabled)
        {
            flashlight.enabled = false;
            flashlightHUD.enabled = flashlight.enabled;
            audio.clip = clickOff;
            audio.Play();
        }
    }
}
