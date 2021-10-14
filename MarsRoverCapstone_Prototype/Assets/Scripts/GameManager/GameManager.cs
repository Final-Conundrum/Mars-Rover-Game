using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(GM_Checkpoint))]
[RequireComponent(typeof(GM_Time))]

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

    [SerializeField] public GameObject player;
    public GameObject postProcessingVolume;

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

        postProcessingVolume.SetActive(Settings.postProcessingActive);
    }

    // Update is called once per frame
    void Update()
    {
        // Testing checkpoints

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(GM_SceneLoader.LoadToScene("Scene_MainGame"));
        }

        postProcessingVolume.SetActive(Settings.postProcessingActive);

    }

    // Collect variables and set the scene upon scene reload
    public void SceneSetup()
    {
        _GM_Checkpoint.safeZones = FindObjectsOfType<CheckpointObject>();

        // Get Player and set
        player = FindObjectOfType<Player_Movement>().gameObject;
        _GM_Audio.player = player;
        GM_Checkpoint.playerCamera = FindObjectOfType<Player_ParentObject>().Camera;

        //_GM_Audio.playerSource = player.GetComponent<AudioSource>();

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

                }
                else if (passTime == 3 || passTime == 0)
                {
                    passTime++;
                    _GM_Popup.FadingPopup("Mars > Jezero Crater \n 1200 hours ", 6f);
                }
                else if (passTime == 4)
                {
                    passTime = 1;
                    _GM_Popup.FadingPopup("Mars > Jezero Crater \n 700 hours ", 6f);

                }

                _GM_Time.SetSceneLights(passTime);
            }
        }
        postProcessingVolume.SetActive(Settings.postProcessingActive);

        _GM_Objectives.FindObjectiveGUI();

        // Animate loading into scene
        GM_SceneLoader.StartScene();
    }
}
