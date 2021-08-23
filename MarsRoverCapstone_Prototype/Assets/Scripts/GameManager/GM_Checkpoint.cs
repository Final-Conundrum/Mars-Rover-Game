using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using Cinemachine;

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

    public GameObject playerCamera;

    // Safe zone variables for setting the environment upon player respawn
    public string SZintroText = "Potential \n > SAFE ZONE <";
    public string SZrebootText = "REBOOT SUCCESSFUL \n >> Be careful out there << \n";

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
        savedAtSafeZone = SZ;

        Debug.Log(gameObject.name + ": Set Checkpoint to " + SZ);
    }

    // Checkpoint conditions are set upon reboot/respawn here
    public void RebootSafeZone()
    {
        currentSafeZone.safeZoneInfo.text = SZrebootText;

        playerCamera = FindObjectOfType<Player_ParentObject>().Camera;
    }
}
