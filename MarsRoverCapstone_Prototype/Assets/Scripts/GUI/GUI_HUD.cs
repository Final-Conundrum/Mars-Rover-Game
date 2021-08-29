using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUI_HUD : MonoBehaviour
{
    Player_Movement PM => FindObjectOfType<Player_Movement>();
    GM_Checkpoint GM => FindObjectOfType<GM_Checkpoint>();

    public TMP_Text health;
    public Slider healthSlider;
    public TMP_Text elevation;
    public Slider elevationSlider;
    public Image elevationSliderHandle;

    public GameObject green_check;
    public GameObject red_check;
    public GameObject info_panel;

    public GameObject inventory;
    public GameObject objectives;

    public TMP_Text mockupPrompt;
    public static TMP_Text staticPrompt;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Set HUD elements
        health.text = " >> INTEGRITY: " + Player_Stats.health + "%";
        healthSlider.value = Player_Stats.health;

        elevation.text = "Ground Dist.: " + Player_Movement.elevation;
        elevationSlider.value = Player_Movement.elevation;

        if(elevationSlider.value < PM.fallDamageHeight)
        {
            elevationSliderHandle.color = Color.green;
        }
        else
        {
            elevationSliderHandle.color = Color.red;

        }

        FallDamageCheck();
    }

    public void FallDamageCheck()
    {
        if (PM.takeFallDamage == false)
        {
            green_check.SetActive(true);
            red_check.SetActive(false);
        }
        else
        {
            red_check.SetActive(true);
            green_check.SetActive(false);
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
