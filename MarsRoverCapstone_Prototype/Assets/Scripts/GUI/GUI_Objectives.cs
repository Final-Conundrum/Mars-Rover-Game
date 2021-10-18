using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI_Objectives : MonoBehaviour
{
    // UI Elements
    public TMP_Text objectivePIXL;
    public TMP_Text objectiveRIMFAX;
    public TMP_Text objectiveDrill;

    public TMP_Text objPIXLCounter;
    public TMP_Text objRIMFAXCounter;
    public TMP_Text objDrillCounter;

    public GameObject expandedObjectiveMenu;
    public TMP_Text expandedPIXL;
    public TMP_Text expandedRIMFAX;
    public TMP_Text expandedDrill;

    // Start is called before the first frame update
    void Awake()
    {
        expandedObjectiveMenu.SetActive(false);

        UpdateObjectives();
    }

    
    // Update is called once per frame
    void Update()
    {
        // Open Expanded Objectives menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (expandedObjectiveMenu.activeSelf == false)
            {
                expandedObjectiveMenu.SetActive(true);
            }
            else if (expandedObjectiveMenu.activeSelf == true)
            {
                expandedObjectiveMenu.SetActive(false);
            }
        }
    }

    // Update all objective UI elements. Called from minigame scripts
    public void UpdateObjectives()
    {
        objectivePIXL.text = GM_Objectives.objectivePIXLString;
        objectiveRIMFAX.text = GM_Objectives.objectiveRIMFAXString;
        objectiveDrill.text = GM_Objectives.objectiveDrillString;

        expandedPIXL.text = "PIXL: Planetary Instrument for X-ray Lithochemistry helps Perseverance understand the chemical history of the minerals it analyzes. \n \n Find exposed minerals in the environment and analyze (" + GM_Objectives._PIXLCurrent + "/" + GM_Objectives._PIXLTotal + ") times.";
        expandedRIMFAX.text = "RIMFAX: Radar Imager for Mars' Subsurface Experiment lets Perseverance see the underground history of Mars. \n \n Find marked scan zones and use RIMFAX (" + GM_Objectives._RIMFAXCurrent + "/" + GM_Objectives._RIMFAXTotal + ") times.";
        expandedDrill.text = "Drill: Uncover samples for future analysis by NASA by drilling delicate exposed minerals on Mars' surface. \n \n Drill for samples(" + GM_Objectives._DrillCurrent + "/" + GM_Objectives._DrillTotal + ") times.";

        objPIXLCounter.text = GM_Objectives._PIXLCurrent + "/" + GM_Objectives._PIXLTotal;
        objRIMFAXCounter.text = GM_Objectives._RIMFAXCurrent + "/" + GM_Objectives._RIMFAXTotal;
        objDrillCounter.text = GM_Objectives._DrillCurrent + "/" + GM_Objectives._DrillTotal;

        Debug.Log("GUI_Objectives: Updated objectives UI elements");
    }

    // Method for UI button to handle opening the objectives menu
    public void OpenObjectives()
    {
        if (expandedObjectiveMenu.activeSelf == false)
        {
            expandedObjectiveMenu.SetActive(true);
        }
        else if (expandedObjectiveMenu.activeSelf == true)
        {
            expandedObjectiveMenu.SetActive(false);
        }
    }
}
