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

    [SerializeField] public GameObject player;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set Cursor to not be visible
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Testing checkpoints
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(Input.GetKeyDown(KeyCode.Equals))
        {
            _GM_Checkpoint.savedAtCheckpoint = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);            
        }

        

        
    }

    // Collect variables and set the scene upon scene reload
    public void SceneSetup()
    {
        _GM_Checkpoint.checkpoints = FindObjectsOfType<CheckpointObject>();

        // Get Player
        player = FindObjectOfType<Player_Movement>().gameObject;

        // Set player position to respawn point
        if (_GM_Checkpoint.savedAtCheckpoint)
        {
            // Disable CC so that checkpoint position may be set
            player.GetComponent<CharacterController>().enabled = false;

            player.transform.position = _GM_Checkpoint.lastCheckpoint;

            player.GetComponent<CharacterController>().enabled = true;
        }

        // Set time of day
        if(passTime == 1)
        {
            passTime++;
        }
        else if(passTime == 2)
        {
            passTime++;
        }
        else if(passTime == 3)
        {
            passTime = 1;
        }

        _GM_Time.SetSceneLights(passTime);
    }
}
