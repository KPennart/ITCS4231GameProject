using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;
    private new AudioSource audio;
    private RawImage flashlightHUD;

    private Texture2D [] flashlightTextures;

    [SerializeField] private AudioClip clickOn;
    [SerializeField] private AudioClip clickOff;

    private float maxFlashlightBattery = 100f;
    private float flashlightBattery = 100f;
    private float flashlightRechargeRate = 0.2f;
    private float flashlightDrainRate = 0.1f;

    private void Start()
    {
        flashlight = GetComponent<Light>();
        audio = GetComponent<AudioSource>();
        flashlightHUD = GameObject.Find("Flashlight HUD").GetComponent<RawImage>();
        flashlightTextures = new Texture2D[]
            {
                (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/flashlight_meter_background.png", typeof(Texture2D)),
                (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/flashlight_meter_20.png", typeof(Texture2D)),
                (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/flashlight_meter_40.png", typeof(Texture2D)),
                (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/flashlight_meter_60.png", typeof(Texture2D)),
                (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/flashlight_meter_80.png", typeof(Texture2D)),
                (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/flashlight_meter_100.png", typeof(Texture2D))
            };
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            
            if (flashlight.enabled)
            {
                flashlightHUD.enabled = true;
            }

            audio.clip = audio.clip == clickOn ? clickOff : clickOn;
            audio.Play();
        }


        CheckForDeadBattery();
        UpdateHUD();


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
    }

    void CheckForDeadBattery()
    {
        if (flashlightBattery == 0 && flashlight.enabled)
        {
            flashlight.enabled = false;
            audio.clip = clickOff;
            audio.Play();
        }
    }

    void UpdateHUD()
    {
        float val = flashlightBattery / maxFlashlightBattery * 6;

        if (val > 5)
        {
            val = 5f;
        }

        //Debug.Log("Battery: " + flashlightBattery + "        Val: " + val);

        flashlightHUD.texture = flashlightTextures[(int)val];

        if (!flashlight.enabled && flashlightBattery == 100)
        {
            flashlightHUD.enabled = false;
        }

    }
}
