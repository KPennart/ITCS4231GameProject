using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController: MonoBehaviour
{

    //declare variables for stamina bar
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina = 100f;
    [SerializeField] private int rechargeCutoff = 50;
    [SerializeField] private float staminaDrainRate = 0.4f;
    [SerializeField] private float staminaRechargeRate = 0.2f;
    private bool staminaRecharging = false;
    public static bool isSprinting = false;
    public static bool canRun = true;
    private Image stamina;
    private Image staminaBackground;

    // Start is called before the first frame update
    void Start()
    {
        //set the stamina bar images to these components
        stamina = GameObject.Find("Stamina Bar").GetComponent<Image>();
        staminaBackground = GameObject.Find("Stamina Bar Background").GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        DrainStamina();
    }

    // Update is called once per frame
    void Update()
    {
        checkForNoStamina();

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && canRun && PlayerAnimationController.isCrouching == false)
        {
            isSprinting = true;
            staminaBackground.enabled = true;
            stamina.enabled = true;
        }
        else
        {
            isSprinting = false;
        }
        stamina.fillAmount = currentStamina / maxStamina;
        updateHUD();
    }

    //drain stamina if player is sprinting, recharge if they are not
    void DrainStamina()
    {
        if (isSprinting)
        {
            currentStamina = Mathf.Max(currentStamina - staminaDrainRate, 0f);
        }
        else
        {
            currentStamina = Mathf.Min(currentStamina + staminaRechargeRate, 100f);
        }
    }

    //check for stamina, if it is depleted, then the player can't run anymore and must wait for stamina cooldown to run again
    void checkForNoStamina()
    {

        //if the stamina was completely depleted and the current stamina is still under 50%, then the stamina is recharging
        if (currentStamina < rechargeCutoff && canRun == false)
        {
            staminaRecharging = true;
        }
        else
        {
            staminaRecharging = false;
        }
        //if stamina is completely depleted or is recharging from total depletion, then the player can't run
        if ((isSprinting && currentStamina == 0) || staminaRecharging)
        {
            canRun = false;
        }
        else
        {
            canRun = true;
        }
    }

    //make stamina bar disappear if the player is not running and the stamina bar is fully recharged
    void updateHUD()
    {
        if (!isSprinting && currentStamina == 100)
        {
            stamina.enabled = false;
            staminaBackground.enabled = false;
        }
    }
}
