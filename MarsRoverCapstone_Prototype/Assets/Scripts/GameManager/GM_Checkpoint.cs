using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public CheckpointObject currentSafeZone;
    public Vector3 currentSafeZonePosition;
    public string SZintroText = "Potential \n > SAFE ZONE <";
    public string SZrebootText = "REBOOT SUCCESSFUL \n >> Be careful out there << \n";

    public CheckpointObject[] safeZones;

    public GameObject player;

    private void Start()
    { 
       foreach(CheckpointObject i in safeZones)
        {
            i.safeZoneInfo.text = SZintroText;
        }

        RebootSafeZone();
    }

    // Checkpoint conditions are set upon reboot/respawn here
    public void RebootSafeZone()
    {
        player = GM.player;
        currentSafeZone.safeZoneInfo.text = SZrebootText;
    }
}
