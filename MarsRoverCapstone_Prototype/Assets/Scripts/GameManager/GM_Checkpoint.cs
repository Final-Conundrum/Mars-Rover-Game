using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GM_Checkpoint : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * GM_Checkpoint: This script handles the saving of checkpoint locations to reload the player and setting up
     * the specific safe zone/checkpoint the player loads to.
     *  
     */
    private GameManager GM => GetComponent<GameManager>();

    public bool savedAtSafeZone = false;

    // Relevent Safe zone objects
    public CheckpointObject currentSafeZone;
    public Vector3 currentSafeZonePosition;
    
    public CheckpointObject[] safeZones;

    public static GameObject playerCamera;
    public TMP_Text SZprompt;

    // Safe zone variables for setting the environment upon player respawn
    public string SZintroText = "Potential \n > SAFE ZONE <";
    public string SZrebootText = "REBOOT SUCCESSFUL \n >> Be careful out there << \n";
    public string[] SZrandomText = { "REBOOT SUCCESSFUL \n>> Be careful out there!", "REBOOT SUCCESSFUL \n>> Remember sand and machine don't mix well, so avoid Mars' dust storms!", "REBOOT SUCCESSFUL \n>> The more we learn, the more we advance, so watch out for minerals and objects to analyze!", "REBOOT SUCCESSFUL \n>> Did you know that Perseverances nickname is Percy?" };

    private void Start()
    { 
        // Set initial text for SZ's
       foreach(CheckpointObject i in safeZones)
        {
            i.safeZoneInfo.text = SZintroText;
        }

        playerCamera = FindObjectOfType<Player_ParentObject>().Camera;
    }

    public void SetSafeZone(CheckpointObject SZ)
    {
        currentSafeZone = SZ;
        currentSafeZonePosition = SZ.transform.position;
        savedAtSafeZone = true;

        Debug.Log(gameObject.name + ": Set Checkpoint to " + SZ);
    }

    // Checkpoint conditions are set upon reboot/respawn here
    public void RebootSafeZone()
    {
        int num = Random.Range(0, SZrandomText.Length - 1);

        currentSafeZone.safeZoneInfo.text = SZrandomText[num];

        playerCamera = FindObjectOfType<Player_ParentObject>().Camera;
        SZprompt = GameObject.FindGameObjectWithTag("StaticPrompt").GetComponent<TMP_Text>();

        foreach (CheckpointObject i in safeZones)
        {
            i.prompt = SZprompt;
        }
    }
}
