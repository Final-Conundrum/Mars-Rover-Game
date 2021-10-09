using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI_Objectives : MonoBehaviour
{
    public TMP_Text objectivePIXL;
    public TMP_Text objectiveRIMFAX;
    public TMP_Text objectiveDrill;

    public GameObject expandedObjectiveMenu;
    public TMP_Text expandedPIXL;
    public TMP_Text expandedRIMFAX;
    public TMP_Text expandedDrill;

    // Start is called before the first frame update
    void Start()
    {
        expandedObjectiveMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && expandedObjectiveMenu.activeSelf == false)
        {
            expandedObjectiveMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && expandedObjectiveMenu.activeSelf == true)
        {
            expandedObjectiveMenu.SetActive(false);

        }

        objectivePIXL.text = GM_Objectives.objectivePIXLString;
        objectiveRIMFAX.text = GM_Objectives.objectiveRIMFAXString;
        objectiveDrill.text = GM_Objectives.objectiveDrillString;

        expandedPIXL.text = "PIXL: Planetary Instrument for X-ray Lithochemistry helps Perseverance understand the chemical history of the minerals it analyzes. \n \n Find exposed minerals in the environment and analyze (" + GM_Objectives._PIXLCurrent + "/" + GM_Objectives._PIXLTotal + ") times.";
        expandedRIMFAX.text = "RIMFAX: Radar Imager for Mars' Subsurface Experiment lets Perseverance see the underground history of Mars. \n \n Find marked scan zones and use RIMFAX (" + GM_Objectives._RIMFAXCurrent + "/" + GM_Objectives._RIMFAXTotal + ") times.";
        expandedDrill.text = "Testing Description";
    }
}
