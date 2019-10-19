using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;
    private new AudioSource audio;

    [SerializeField] private AudioClip clickOn;
    [SerializeField] private AudioClip clickOff;

    private void Start()
    {
        flashlight = GetComponent<Light>();
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            audio.clip = audio.clip == clickOn ? clickOff : clickOn;
            audio.Play();
        }
    }
}
