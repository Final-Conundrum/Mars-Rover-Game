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
     * Interact with specified GM_ scripts.
     * 
     * 
     */

    private static GameManager instance;
    private GM_Checkpoint _GM_Checkpoint => GetComponent<GM_Checkpoint>();
    private GM_Time _GM_Time => GetComponent<GM_Time>();

    private GameObject player => FindObjectOfType<Player_Movement>().gameObject;

    private void Awake()
    {
        if(instance == null)
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
        player.transform.position = _GM_Checkpoint.lastCheckpoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
