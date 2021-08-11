using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckpointObject : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * CheckpointObject: Attached to the trigger for the safe zone/checkpoint.
     * This is called by GM_Checkpoint to set this safe zone with the appropriate data.
     * 
     * i.e: inventory storage locker. 
     */

    // Find GameManager of checkpoints
    private GM_Checkpoint GM => FindObjectOfType<GM_Checkpoint>();

    // Aesthetics of checkpoints
    public Material untriggeredCheckpoint;
    public Material triggeredCheckedPoint;
    private MeshRenderer mesh => GetComponent<MeshRenderer>();

    public TMP_Text safeZoneInfo;

    // Start is called before the first frame update
    void Start()
    {
        mesh.material = untriggeredCheckpoint;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mesh.material = triggeredCheckedPoint;
            GM.currentSafeZone = this;
            GM.currentSafeZonePosition = this.transform.position;
            GM.savedAtSafeZone = true;    

            Debug.Log(gameObject.name + ": Set Checkpoint to " + GM.currentSafeZone);
        }
    }
}
