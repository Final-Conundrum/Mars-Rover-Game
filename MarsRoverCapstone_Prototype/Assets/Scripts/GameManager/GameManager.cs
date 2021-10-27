using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(GM_Checkpoint))]
[RequireComponent(typeof(GM_Time))]
[RequireComponent(typeof(GM_Audio))]
[RequireComponent(typeof(GM_Popup))]
[RequireComponent(typeof(GM_Objectives))]

public class GameManager : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * GameManager:
     * Handles scenewide events for loading and readying elements for play.
     * Interacts with specified GM_ scripts.
     * 
     * Will handle technical jobs and broader systems.
     */

    private static GameManager instance;

    // Get other GM_ scripts on object
    private GM_Checkpoint _GM_Checkpoint => GetComponent<GM_Checkpoint>();
    private GM_Time _GM_Time => GetComponent<GM_Time>();
    private GM_Popup _GM_Popup => GetComponent<GM_Popup>();
    private GM_Audio _GM_Audio => GetComponent<GM_Audio>();
    private GM_Objectives _GM_Objectives => GetComponent<GM_Objectives>();

    [Space]
    [Header("Static Player Objects")]
    [SerializeField] public GameObject player;
    public static GameObject playerCamera;
    public CinemachineVirtualCamera introCamera;

    [Space]
    [Header("Post Processing Variables")]
    public PostProcessVolume PP_Volume;
    public PostProcessProfile PP_day;
    public PostProcessProfile PP_night;

    private GeyserBurst[] geysers => FindObjectsOfType<GeyserBurst>();

    // Passage of Time variables
    public bool usePassageOfTime = true;
    public static float passTime = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        // Get Player
        player = FindObjectOfType<Player_Movement>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set Cursor to not be visible
        //Cursor.visible = false;
        playerCamera = FindObjectOfType<Player_ParentObject>().Camera;

        PP_Volume.gameObject.SetActive(Settings.postProcessingActive);

        introCamera.Priority = 20;
    }

    // Update is called once per frame
    void Update()
    {
        // Testing checkpoints

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(GM_SceneLoader.LoadToScene("Scene_MainGame"));
        }

        PP_Volume.gameObject.SetActive(Settings.postProcessingActive);

    }

    // Collect variables and set the scene upon scene reload
    public void SceneSetup()
    {
        _GM_Checkpoint.safeZones = FindObjectsOfType<CheckpointObject>();

        // Get Player and set
        player = FindObjectOfType<Player_Movement>().gameObject;
        _GM_Audio.player = player;

        // Setup camera transiton
        playerCamera = FindObjectOfType<Player_ParentObject>().Camera;
        introCamera.Priority = 20;

        // Set player position to respawn point
        if (_GM_Checkpoint.savedAtSafeZone)
        {
            // Disable CC so that checkpoint position may be set
            player.GetComponent<CharacterController>().enabled = false;

            player.transform.position = _GM_Checkpoint.currentSafeZonePosition;

            player.GetComponent<CharacterController>().enabled = true;

            // Set Safezone reboot screen
            _GM_Checkpoint.RebootSafeZone();

            // Set time of day
            if (usePassageOfTime)
            {
                if (passTime == 1)
                {
                    passTime++;
                    _GM_Popup.FadingPopup("Mars > Jezero Crater \n 1650 hours ", 6f);
                }
                else if (passTime == 2)
                {
                    passTime++;
                    _GM_Popup.FadingPopup("Mars > Jezero Crater \n 2200 hours ", 6f);
                    PP_Volume.profile = PP_night;

                }
                else if (passTime == 3 || passTime == 0)
                {
                    passTime++;
                    _GM_Popup.FadingPopup("Mars > Jezero Crater \n 1200 hours ", 6f);
                    PP_Volume.profile = PP_day;
                }
                else if (passTime == 4)
                {
                    passTime = 1;
                    _GM_Popup.FadingPopup("Mars > Jezero Crater \n 700 hours ", 6f);

                }

                _GM_Time.SetSceneLights(passTime);
            }
        }
        PP_Volume.gameObject.SetActive(Settings.postProcessingActive);

        _GM_Objectives.FindObjectiveGUI();

        // Animate loading into scene
        GM_SceneLoader.StartScene();
        introCamera.Priority = 0;

        GM_Objectives.playerPopup = FindObjectOfType<GUI_infoPanel>();
    }
}
