using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Test_GUI_HUD : MonoBehaviour
{
    Player_Movement PM => FindObjectOfType<Player_Movement>();
    
    public TMP_Text health;
    public TMP_Text elevation;

    public GameObject green_check;
    public GameObject red_check;
    public GameObject info_panel;


    // Start is called before the first frame update
    void Start()
    {
        health.text = "Hello there";
        elevation.text = "goodbye";
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + Player_Stats.health;
        elevation.text = "Elevation: " + Player_Movement.elevation;
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
        info_panel.SetActive(false);
    }
   
    
}
