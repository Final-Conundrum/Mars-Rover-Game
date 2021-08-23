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

    private static GameObject instance;

    // Find GameManager of checkpoints
    private GM_Checkpoint GM => FindObjectOfType<GM_Checkpoint>();

    // Aesthetics of checkpoints
    public Canvas canvas;
    // Overhead icon
    public Image icon;
    public Sprite untriggeredCheckpoint;
    public Sprite triggeredCheckedPoint;

    // Informational panel
    public TMP_Text safeZoneInfo;
    public Image background;
    public float distanceToDisplay = 10f;

    // Objects
    public GameObject flag;

    private void Awake()
    {
        
    }

    void Start()
    {
        // Reset SZ icon
        icon.sprite = untriggeredCheckpoint;
        flag.SetActive(false);
    }

    private void Update()
    {
        canvas.transform.LookAt(GM.playerCamera.transform);
        //canvas.transform.rotation = Quaternion.Euler(0f, canvas.transform.rotation.y, 0f);
        //canvas.transform.rotation = Quaternion.Euler(0f, GM.playerCamera.transform.rotation.y, 0f);

        // Choose to display SZ Panel depending on player distance
        if (Vector3.Distance(transform.position, Player_ParentObject.staticCamera.transform.position) > distanceToDisplay)
        {
            SZPanelAppearance(false);
        }
        else
        {
            SZPanelAppearance(true);
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        // Prompt player to set safe zone
        if (collision.gameObject.tag == "Player") 
        {
            // Display prompt
            // GUI_infoPrompt.CheckPointText()
            if(Input.GetKeyDown(KeyCode.E))
            {
                icon.sprite = triggeredCheckedPoint;
                GM.SetSafeZone(this);
                flag.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        

    }

    public void SZPanelAppearance(bool visible)
    {
        safeZoneInfo.enabled = visible;
        background.enabled = visible;
    }
}
