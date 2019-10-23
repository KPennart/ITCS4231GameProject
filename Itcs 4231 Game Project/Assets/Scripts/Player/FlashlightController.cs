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
            flashlightHUD.enabled = !flashlightHUD.enabled;

            audio.clip = audio.clip == clickOn ? clickOff : clickOn;
            audio.Play();
        }
    }
}
