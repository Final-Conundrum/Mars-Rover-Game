using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUI_HUD : MonoBehaviour
{
    Player_Movement PM => FindObjectOfType<Player_Movement>();
    GM_Checkpoint GM => FindObjectOfType<GM_Checkpoint>();

    public Animator transitionAnimation;

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
    public GameObject playerHUD;

    public GameObject info_panel;
    public GameObject chooseControlsPanel;

    public GameObject inventory;

    public TMP_Text mockupPrompt;
    public static TMP_Text staticPrompt;

    // Reusable colour values
    private Color greenBoost = new Color(0f, 150f, 0f, 0.6f);
    private Color redBoost = new Color(255f, 0, 0, 0.6f);

    public GameObject damageOverlay;
    private float damageTimer = 1f;

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

            chooseControlsPanel.SetActive(true);

            playerHUD.SetActive(false);
        }

        staticPrompt = mockupPrompt;

        // Set values of Boost Slider
        boostSlider.maxValue = PM.boostLimit;
        boostSlider.minValue = 0f;

        damageOverlay.SetActive(false);

        transitionAnimation.SetBool("Transition", true);
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
            boosterSliderColour.color = greenBoost;
        }
        else
        {
            boosterSliderColour.color = redBoost;
        }

        boostSlider.value = PM.boost;
    }

    // Close Learning Panel and Activate HUD
    public void ClosePanel()
    {
        Time.timeScale = 1;

        Cursor.visible = false;

        GUI_PauseMenu.pausedGame = false;

        info_panel.SetActive(false);
        chooseControlsPanel.SetActive(false);

        playerHUD.SetActive(true);
    }

    public void ChooseControls(bool tankControls)
    {
        PM.tankControls = tankControls;
        chooseControlsPanel.SetActive(false);
    }

    public IEnumerator DamagePlayer()
    {
        damageOverlay.SetActive(true);
        yield return new WaitForSeconds(damageTimer);
        damageOverlay.SetActive(false);
    }
}
