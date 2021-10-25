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
    [Space]
    [Header("Found SZ in scene (non-interactable)")]
    public CheckpointObject currentSafeZone;
    public Vector3 currentSafeZonePosition;
    
    public CheckpointObject[] safeZones;

    public TMP_Text SZprompt;

    // Safe zone variables for setting the environment upon player respawn
    [Space]
    [Header("SZ info variables")]
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
        Physical_Inventory inventory = FindObjectOfType<Physical_Inventory>();

        foreach (Inventory_Slots i in Physical_Inventory.inventory.itemContainer)
        {
            if (i.item.name == "RandomMineral")
            {
                GM_Objectives.UpdateObjective("PIXL", i.amount);
            }

            if (i.item.name == "Drill_Item")
            {
                GM_Objectives.UpdateObjective("Drill", i.amount);
            }

            if (i.item.name == "RIMFAX_Item")
            {
                GM_Objectives.UpdateObjective("RIMFAX", i.amount);
            }

        }

        inventory.ClearInventory();

        int num = Random.Range(0, SZrandomText.Length - 1);

        currentSafeZone.safeZoneInfo.text = SZrandomText[num];

    }
}
