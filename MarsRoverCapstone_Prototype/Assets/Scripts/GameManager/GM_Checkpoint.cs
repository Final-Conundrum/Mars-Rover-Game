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

    public bool savedAtSafeZone = false;
    public CheckpointObject currentSafeZone;
    public Vector3 currentSafeZonePosition;

    public CheckpointObject[] safeZones;

    private void Start()
    {
       foreach(CheckpointObject i in safeZones)
        {
            i.safeZoneInfo.text = "Potential \n > SAFE ZONE <";
        }
    }

    public void RebootSafeZone()
    {
        currentSafeZone.safeZoneInfo.text = "REBOOT SUCCESSFUL \n >> Be careful out there << \n";
    }
}
