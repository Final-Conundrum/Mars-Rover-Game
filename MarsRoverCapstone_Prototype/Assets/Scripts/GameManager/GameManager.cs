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

    public GameObject player;

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
    }

    public void SceneSetup()
    {
        Debug.Log("GameManager: Setup scene.");

        // Get Player
        player = FindObjectOfType<Player_Movement>().gameObject;

    }
}
