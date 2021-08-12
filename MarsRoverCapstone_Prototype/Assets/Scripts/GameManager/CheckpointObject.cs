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
    public Image icon;

    public Sprite untriggeredCheckpoint;
    public Sprite triggeredCheckedPoint;

    public TMP_Text safeZoneInfo;
    public float distanceToDisplay = 10f;

    // Start is called before the first frame update
    void Start()
    {
        icon.sprite = untriggeredCheckpoint;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, GM.player.transform.position) > distanceToDisplay)
        {
            safeZoneInfo.enabled = false;
        }
        else
        {
            safeZoneInfo.enabled = true;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            icon.sprite = triggeredCheckedPoint;
            GM.currentSafeZone = this;
            GM.currentSafeZonePosition = this.transform.position;
            GM.savedAtSafeZone = true;    

            Debug.Log(gameObject.name + ": Set Checkpoint to " + GM.currentSafeZone);
        }
    }
}
