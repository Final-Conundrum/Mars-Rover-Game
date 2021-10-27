using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
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
    public AudioSource audioSource => GetComponent<AudioSource>();
    public static Inventory_Systems inventory_system; 

    // Aesthetics of checkpoints
    public Canvas canvas;
    // Overhead icon
    public Image icon;

    // Informational panel
    public TMP_Text safeZoneInfo;
    public Image background;
    public float distanceToDisplay = 10f;

    // Objects
    public GameObject flag;

    void Start()
    {
        // Reset SZ
        flag.SetActive(false);
    }

    private void Update()
    {
        // Choose to display SZ Panel depending on player distance
        if (Vector3.Distance(transform.position, Player_ParentObject.staticCamera.transform.position) > distanceToDisplay)
        {
            SZPanelAppearance(false, new Vector3(1f,1f,1f));
        }
        else
        {
            float dist = (Vector3.Distance(transform.position, GameManager.playerCamera.transform.position)) / 30;
            SZPanelAppearance(true, new Vector3(dist, dist, dist));
        }

        // Make canvas format to the camera position
        canvas.transform.LookAt(GameManager.playerCamera.transform);
    }


    // Collision with player events
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            // Display prompt
            if(GM.currentSafeZone != this)
            {
                GUI_HUD.UpdatePrompt("E", "Set Safe Zone here", true);
            }
            else if(GM.currentSafeZone == this)
            {
                GUI_HUD.UpdatePrompt("H", "UPLOAD FINDINGS & to pass the time", true);
            }
        }
    }

    public void OnTriggerStay(Collider col)
    {
        // Prompt player to set safe zone
        if (col.gameObject.tag == "Player") 
        {
            if (Input.GetKeyDown(KeyCode.E) && GM.currentSafeZone != this)
            {
                GM.SetSafeZone(this);
                flag.SetActive(true);
                icon.gameObject.SetActive(false);

                safeZoneInfo.text = "This is Perseverance's current Reboot area";

                GM_Audio.PlaySound(audioSource, "MGWin");
            }
            
            if (GM.currentSafeZone == this)
            {
                GUI_HUD.UpdatePrompt("H", "UPLOAD FINDINGS & to pass the time", true);

                if (Input.GetKeyDown(KeyCode.H))
                {
                    safeZoneInfo.text = "Upload to NASA successful";
                    GM.SetSafeZone(this);
                    GM.RebootSafeZone();

                    StartCoroutine(GM_SceneLoader.LoadToScene("Scene_MainGame"));
                }
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Display prompt
            GUI_HUD.UpdatePrompt("", "", false);
        }
    }

    public void SZPanelAppearance(bool visible, Vector3 size)
    {
        background.gameObject.SetActive(visible);
        background.transform.localScale = size;
    }
}
