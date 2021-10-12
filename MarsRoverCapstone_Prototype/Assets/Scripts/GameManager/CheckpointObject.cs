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

    // Aesthetics of checkpoints
    public Canvas canvas;
    // Overhead icon
    public Image icon;

    // Informational panel
    public TMP_Text safeZoneInfo;
    public Image background;
    public float distanceToDisplay = 10f;

    public TMP_Text prompt;

    // Objects
    public GameObject flag;

    void Start()
    {
        // Reset SZ
        flag.SetActive(false);
        prompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Make canvas format to the camera position
        canvas.transform.LookAt(GM_Checkpoint.playerCamera.transform);

        // Choose to display SZ Panel depending on player distance
        if (Vector3.Distance(transform.position, Player_ParentObject.staticCamera.transform.position) > distanceToDisplay)
        {
            SZPanelAppearance(false, new Vector3(1f,1f,1f));
        }
        else
        {
            float dist = (Vector3.Distance(transform.position, GM_Checkpoint.playerCamera.transform.position)) / 30;
            SZPanelAppearance(true, new Vector3(dist, dist, dist));
        }
    }


    // Collision with player events
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            // Display prompt
            prompt.gameObject.SetActive(true);

            if(GM.currentSafeZone != this)
            {
                prompt.text = "Press [E] to set Safe Zone here...";
            }
            else if(GM.currentSafeZone == this)
            {
                prompt.text = "Press [H] to pass the time...";
            }
        }
    }

    public void OnTriggerStay(Collider col)
    {
        // Prompt player to set safe zone
        if (col.gameObject.tag == "Player") 
        {

            if(Input.GetKeyDown(KeyCode.E) && GM.currentSafeZone != this)
            {
                GM.SetSafeZone(this);
                flag.SetActive(true);
                icon.gameObject.SetActive(false);

                safeZoneInfo.text = "<< SAFE ZONE >> \n This is Perseverance's current Reboot area";

                GM_Audio.PlaySound(audioSource, "MGWin");
            }
            else if (Input.GetKeyDown(KeyCode.H) && GM.currentSafeZone == this)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Display prompt
            prompt.gameObject.SetActive(false);
        }
    }

    public void SZPanelAppearance(bool visible, Vector3 size)
    {
        background.gameObject.SetActive(visible);
        background.transform.localScale = size;
    }
}
