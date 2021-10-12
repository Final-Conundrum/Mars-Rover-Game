using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GM_Objectives : MonoBehaviour
{
    // String used for UI objective display
    public static string objectivePIXLString, objectiveRIMFAXString, objectiveDrillString;

    // Current completed minigames and total needed to finish
    private int PIXLCurrent, RIMFAXCurrent, DrillCurrent;
    public int PIXLTotal, RIMFAXTotal, DrillTotal;

    public static int _PIXLCurrent, _PIXLTotal, _RIMFAXCurrent, _RIMFAXTotal, _DrillCurrent, _DrillTotal;
    public static bool completedObjectives = false;

    // Start is called before the first frame update
    void Start()
    {
        _PIXLCurrent = PIXLCurrent;
        _PIXLTotal = PIXLTotal;
        _RIMFAXCurrent = RIMFAXCurrent;
        _RIMFAXTotal = RIMFAXTotal;
        _DrillCurrent = DrillCurrent;
        _DrillTotal = DrillTotal;

        objectivePIXLString = "> Analyze minerals using the PIXL (" + _PIXLCurrent + "/" + _PIXLTotal + ")";
        objectiveRIMFAXString = "> Use RIMFAX to construct underground scans (" + _RIMFAXCurrent + "/" + _RIMFAXTotal + ")";
        objectiveDrillString = "> Use the Drill to clear and collect samples (" + _DrillCurrent + "/" + _DrillTotal + ")";
    }
        
    public static void UpdateObjective(string objectiveType)
    {
        switch(objectiveType)
        {
            case "PIXL":
                _PIXLCurrent++;
                objectivePIXLString = "> Analyze minerals using the PIXL (" + _PIXLCurrent + "/" + _PIXLTotal + ")";

                break;
            case "RIMFAX":
                _RIMFAXCurrent++;
                objectiveRIMFAXString = "> Use RIMFAX to construct underground scans (" + _RIMFAXCurrent + "/" + _RIMFAXTotal + ")";

                break;
            case "Drill":
                _DrillCurrent++;
                objectiveDrillString = "> Use the Drill to clear and collect samples (" + _DrillCurrent + "/" + _DrillTotal + ")";

                break;
        }

        if(_PIXLCurrent == _PIXLTotal && _RIMFAXCurrent == _RIMFAXTotal && _DrillCurrent == _DrillTotal)
        {
            completedObjectives = true;
        }
    }
}
