using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations;

public class CheckpointObject : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * CheckpointObject: Attached to the trigger for the safe zone/checkpoint.
     * This is called by GM_Checkpoint to set this safe zone with the appropriate data.
     * 
     * i.e: inventory storage locker. 
     */

    private static CheckpointObject instance;

    // Find GameManager of checkpoints
    private GM_Checkpoint GM => FindObjectOfType<GM_Checkpoint>();

    // Aesthetics of checkpoints
    public Image icon;

    public Sprite untriggeredCheckpoint;
    public Sprite triggeredCheckedPoint;

    public TMP_Text safeZoneInfo;
    public Image background;
    public float distanceToDisplay = 10f;

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

    void Start()
    {
        // Reset SZ icon
        icon.sprite = untriggeredCheckpoint;
    }

    private void Update()
    {
        // Choose to display SZ Panel depending on player distance
        if (Vector3.Distance(transform.position, GM.player.transform.position) > distanceToDisplay)
        {
            safeZoneInfo.enabled = false;
            background.enabled = false;
        }
        else
        {
            safeZoneInfo.enabled = true;
            background.enabled = true;
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        // Prompt player to set safe zone
        if (collision.gameObject.tag == "Player") 
        {
            GUI_HUD.DisplayPrompt(true, "Set your current 'Safe Zone'");
            if(Input.GetKeyDown(KeyCode.E))
            {
                icon.sprite = triggeredCheckedPoint;
                GM.SetSafeZone(this);
            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GUI_HUD.DisplayPrompt(false, "Set your current 'Safe Zone'");
        }
    }
}
