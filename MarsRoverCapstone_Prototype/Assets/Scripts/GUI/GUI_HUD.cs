using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUI_HUD : MonoBehaviour
{
    Player_Movement PM => FindObjectOfType<Player_Movement>();
    GM_Checkpoint GM => FindObjectOfType<GM_Checkpoint>();

    // UI Sliders
    public TMP_Text health;
    public Slider healthSlider;

    public TMP_Text elevation;
    public Slider elevationSlider;
    public Image elevationSliderHandle;
    public Image elevationSliderColour;

    public Slider boostSlider;
    public Image boostSliderHandle;
    public Image boosterSliderColour;

    // UI Elements
    public GameObject info_panel;

    public GameObject inventory;
    public GameObject objectives;

    public TMP_Text mockupPrompt;
    public static TMP_Text staticPrompt;

    // Reusable colour values
    private Color green = new Color(0, 255f, 0, 50f);
    private Color red = new Color(255f, 0, 0, 50f);

    // Start is called before the first frame update
    void Start()
    {
        // Set appropriate elements when starting scene
        if(GM.savedAtSafeZone)
        {
            ClosePanel();
        }
        else
        {
            GUI_PauseMenu.pausedGame = true;

            Cursor.visible = true;
            Time.timeScale = 0;

            info_panel.SetActive(true);

            health.gameObject.SetActive(false);
            elevation.gameObject.SetActive(false);

            inventory.SetActive(false);
            objectives.SetActive(false);
        }

        staticPrompt = mockupPrompt;

        // Set values of Boost Slider
        boostSlider.maxValue = PM.boostLimit;
        boostSlider.minValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Set HUD elements
        // Update Health values
        health.text = " >> INTEGRITY: " + Player_Stats.health + "%";
        healthSlider.value = Player_Stats.health;

        // Update Boost slider
        if(PM.boost > 0.5f) 
        {
            boostSliderHandle.color = Color.green;
            boosterSliderColour.color = Color.green;
        }
        else
        {
            boostSliderHandle.color = Color.red;
            boosterSliderColour.color = Color.red;
        }

        boostSlider.value = PM.boost;

        // Update Elevation information and fall damge
        elevation.text = "Ground Dist.: " + Player_Movement.elevation;
        elevationSlider.value = Player_Movement.elevation;

        if(elevationSlider.value < PM.fallDamageHeight)
        {
            elevationSliderHandle.color = green;
        }
        else
        {
            elevationSliderHandle.color = red;
        }
        FallDamageCheck();
    }

    public void FallDamageCheck()
    {
        if (PM.takeFallDamage == false)
        {
            elevationSliderColour.color = green;
        }
        else
        {
            elevationSliderColour.color = red;
        }
    }

    public void ClosePanel()
    {
        Time.timeScale = 1;

        Cursor.visible = false;

        GUI_PauseMenu.pausedGame = false;

        info_panel.SetActive(false);

        inventory.SetActive(true);
        objectives.SetActive(true);
        health.gameObject.SetActive(true);
        elevation.gameObject.SetActive(true);
    }
}
