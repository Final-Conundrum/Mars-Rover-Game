using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GM_Objectives : MonoBehaviour
{
    public static string objectivePIXLString;
    private int PIXLCurrent;
    public static int _PIXLCurrent;
    public int PIXLTotal;
    public static int _PIXLTotal;

    public static string objectiveRIMFAXString;
    private int RIMFAXCurrent;
    public static int _RIMFAXCurrent;
    public int RIMFAXTotal;
    public static int _RIMFAXTotal;

    public static string objectiveDrillString;
    private int DrillCurrent;
    public static int _DrillCurrent;
    public int DrillTotal;
    public static int _DrillTotal;

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

    // Update is called once per frame
    void Update()
    {

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
    }
}
