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

    // Get GM_ scripts on object
    private GM_Checkpoint _GM_Checkpoint => GetComponent<GM_Checkpoint>();
    private GM_Time _GM_Time => GetComponent<GM_Time>();

    [SerializeField] private GameObject player;

    private float timer = 1f;

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
        Cursor.visible = false;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Collect variables and set the scene upon scene reload
    public void SceneSetup()
    {
        Debug.Log("GameManager: Setup scene.");

        timer = Time.time + 1f;

        _GM_Checkpoint.checkpoints = FindObjectsOfType<CheckpointObject>();

        // Get Player
        player = FindObjectOfType<Player_Movement>().gameObject;
        player.GetComponent<CharacterController>().enabled = false;

        // Set player position to respawn point
        if (_GM_Checkpoint.savedAtCheckpoint)
        {
            player.transform.position = _GM_Checkpoint.lastCheckpoint;
        }

        if (Time.time >= timer)
        {
            player.GetComponent<CharacterController>().enabled = true;

        }
    }
}
