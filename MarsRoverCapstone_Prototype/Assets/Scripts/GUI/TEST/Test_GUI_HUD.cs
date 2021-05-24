using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Test_GUI_HUD : MonoBehaviour
{
    public TMP_Text health;
    public TMP_Text elevation;

    public static GameObject green_check;
    public static GameObject red_check;

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
    }



    public static void Set_Green() 
    {
        green_check.SetActive(false);
        
    }

    public static void Set_Red()
    {
        red_check.SetActive(true);


    }
}
