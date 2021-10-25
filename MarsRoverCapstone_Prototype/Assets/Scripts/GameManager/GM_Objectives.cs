using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GM_Objectives : MonoBehaviour
{
    public static GUI_Objectives ObjectiveGUI;
    public static GUI_infoPanel playerPopup;

    // Objective audio
    public AudioSource audioSource => GetComponent<AudioSource>();
    public static AudioSource _audioSource;
    public AudioClip MG_PIXL_TTS, MG_RIMFAX_TTS, MG_Drill_TTS, Obj_PIXL_TTS, Obj_RIMFAX_TTS, Obj_Drill_TTS;
    public static AudioClip _MG_PIXL_TTS, _MG_RIMFAX_TTS, _MG_Drill_TTS, _Obj_PIXL_TTS, _Obj_RIMFAX_TTS, _Obj_Drill_TTS;

    // String used for UI objective display
    public static string objectivePIXLString, objectiveRIMFAXString, objectiveDrillString;

    // Current completed minigames and total needed to finish
    private int PIXLCurrent, RIMFAXCurrent, DrillCurrent;
    public int PIXLTotal, RIMFAXTotal, DrillTotal;

    public static int _PIXLCurrent, _PIXLTotal, _RIMFAXCurrent, _RIMFAXTotal, _DrillCurrent, _DrillTotal;
    public static bool completedObjectives = false;

    public static bool updatingObjectives = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize static variables
        _PIXLCurrent = PIXLCurrent;
        _PIXLTotal = PIXLTotal;
        _RIMFAXCurrent = RIMFAXCurrent;
        _RIMFAXTotal = RIMFAXTotal;
        _DrillCurrent = DrillCurrent;
        _DrillTotal = DrillTotal;

        _audioSource = audioSource;
        _MG_PIXL_TTS = MG_PIXL_TTS;
        _MG_RIMFAX_TTS = MG_RIMFAX_TTS;
        _MG_Drill_TTS = MG_Drill_TTS;
        _Obj_PIXL_TTS = Obj_PIXL_TTS;
        _Obj_RIMFAX_TTS = Obj_RIMFAX_TTS;
        _Obj_Drill_TTS = Obj_Drill_TTS;

        objectivePIXLString = "> Analyze minerals using the PIXL (" + _PIXLCurrent + "/" + _PIXLTotal + ")";
        objectiveRIMFAXString = "> Use RIMFAX to construct underground scans (" + _RIMFAXCurrent + "/" + _RIMFAXTotal + ")";
        objectiveDrillString = "> Use the Drill to clear and collect samples (" + _DrillCurrent + "/" + _DrillTotal + ")";

        FindObjectiveGUI();
        ObjectiveGUI.UpdateObjectives();
    }

    public static void UpdateObjective(string objectiveType, int amount)
    {
        switch(objectiveType)
        {
            case "PIXL":
                _PIXLCurrent += amount;
                objectivePIXLString = "> Analyze minerals using the PIXL (" + _PIXLCurrent + "/" + _PIXLTotal + ")";
                _audioSource.PlayOneShot(_MG_PIXL_TTS);

                break;
            case "RIMFAX":
                _RIMFAXCurrent += amount;
                objectiveRIMFAXString = "> Use RIMFAX to construct underground scans (" + _RIMFAXCurrent + "/" + _RIMFAXTotal + ")";
                _audioSource.PlayOneShot(_MG_RIMFAX_TTS);

                break;
            case "Drill":
                _DrillCurrent += amount;
                objectiveDrillString = "> Use the Drill to clear and collect samples (" + _DrillCurrent + "/" + _DrillTotal + ")";
                _audioSource.PlayOneShot(_MG_Drill_TTS);

                break;
        }

        if(_PIXLCurrent == _PIXLTotal && _RIMFAXCurrent == _RIMFAXTotal && _DrillCurrent == _DrillTotal)
        {
            completedObjectives = true;
        }

        ObjectiveGUI.UpdateObjectives();
        playerPopup.StartCoroutine(GUI_infoPanel.ImageFadeout());
    }

    public void FindObjectiveGUI()
    {
        ObjectiveGUI = FindObjectOfType<GUI_Objectives>();
    }
}
