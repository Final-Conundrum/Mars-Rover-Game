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
    public static bool endOfGame = false;

    // Result images used by MiniGame_Results, which interacts with attached script MiniGame_ResultCheck

    public GameObject[] PIXL_ImagePrefab;
    public static GameObject[] _PIXL_ImagePrefab;
    public GameObject[] RIMFAX_ImagePrefab;
    public static GameObject[] _RIMFAX_ImagePrefab;

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

        _PIXL_ImagePrefab = PIXL_ImagePrefab;
        _RIMFAX_ImagePrefab = RIMFAX_ImagePrefab;

        objectivePIXLString = "> Analyze minerals using the PIXL (" + _PIXLCurrent + "/" + _PIXLTotal + ")";
        objectiveRIMFAXString = "> Use RIMFAX to construct underground scans (" + _RIMFAXCurrent + "/" + _RIMFAXTotal + ")";
        objectiveDrillString = "> Use the Drill to clear and collect samples (" + _DrillCurrent + "/" + _DrillTotal + ")";

        FindObjectiveGUI();
        ObjectiveGUI.UpdateObjectives();

        // Test ending of game
        //completedObjectives = true;
    }

    public static void UpdateObjective(string objectiveType, int amount)
    {
        switch(objectiveType)
        {
            case "PIXL":
                _PIXLCurrent += amount;
                objectivePIXLString = "> Analyze minerals using the PIXL (" + _PIXLCurrent + "/" + _PIXLTotal + ")";

                if(_PIXLCurrent == _PIXLTotal)
                {
                    _audioSource.PlayOneShot(_Obj_PIXL_TTS);
                }

                break;
            case "RIMFAX":
                _RIMFAXCurrent += amount;
                objectiveRIMFAXString = "> Use RIMFAX to construct underground scans (" + _RIMFAXCurrent + "/" + _RIMFAXTotal + ")";

                if(_RIMFAXCurrent == _RIMFAXTotal)
                {
                    _audioSource.PlayOneShot(_MG_RIMFAX_TTS);
                }

                break;
            case "Drill":
                _DrillCurrent += amount;
                objectiveDrillString = "> Use the Drill to clear and collect samples (" + _DrillCurrent + "/" + _DrillTotal + ")";

                if(_DrillCurrent == _DrillTotal)
                {
                    _audioSource.PlayOneShot(_MG_Drill_TTS);

                }
                break;
        }

        if(_PIXLCurrent >= _PIXLTotal && _RIMFAXCurrent >= _RIMFAXTotal && _DrillCurrent >= _DrillTotal)
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

    public static void CompleteMGTTS(string objectiveType)
    {
        switch (objectiveType)
        {
            case "PIXL":
                _audioSource.PlayOneShot(_MG_PIXL_TTS);

                break;
            case "RIMFAX":
                _audioSource.PlayOneShot(_MG_RIMFAX_TTS);

                break;
            case "Drill":
                _audioSource.PlayOneShot(_MG_Drill_TTS);
                break;
        }
    }
}
