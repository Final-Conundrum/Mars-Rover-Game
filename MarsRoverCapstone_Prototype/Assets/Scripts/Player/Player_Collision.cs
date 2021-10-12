using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    Player_Movement PM => GetComponent<Player_Movement>();
    //Player_Stats _Player_Stats => GetComponent<Player_Stats>();
    Rigidbody RB => GetComponent<Rigidbody>();
    MiniGame_Systems MiniGame => FindObjectOfType<MiniGame_Systems>();
    GUI_infoPanel InfoPanel => FindObjectOfType<GUI_infoPanel>();

    private float exitPosY;
    private bool jumpingFromGeyser = false;

    private void Update()
    {
        // Coyote Time: Allow player to press jump button a few frames after leaving ground
        if (Time.time > Player_Movement.coyoteTime)
        {
            Player_Movement.grounded = false;
        }
    }

    // COLLISION Detection
    private void OnCollisionEnter(Collision c)
    {
        // Checks for ground collision and whether to damage player with fall damage
        if (c.gameObject.tag == "Ground" && PM.takeFallDamage && !jumpingFromGeyser)
        {
            if(transform.position.y <= exitPosY - PM.fallDamageHeight)
            {
                Player_Stats.TakeDamage(25);
                Player_Movement.grounded = true;

                Debug.Log(gameObject.name + ": Player_Collision, Player should take fall damage here...");
            }
        }

        if (c.gameObject.tag == "Ground")
        {
            //GM_Audio.StopSound(PM.audioSource);
            PM.audioSource.clip = GM_Audio.drivingSFX;
            PM.audioSource.Play();
        }

        if (c.gameObject.tag == "Hazard")
        {
            Player_Stats.TakeDamage(10);
        }
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
            // Handle Coyote time
            Player_Movement.coyoteTime = Time.time + PM._coyoteTime;
            Player_Movement.grounded = true;
            jumpingFromGeyser = false;

            if(!PM.audioSource.isPlaying)
            {
                PM.audioSource.clip = GM_Audio.drivingSFX;
                PM.audioSource.Play();
            }
        }
    }

    private void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
            Player_Movement.grounded = false;
            exitPosY = transform.position.y;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Geyser")
        {
            //PM.jumpHeight = PM.geyserJumpHeight;
            //PM._CCMovement.y = PM.geyserJumpHeight;
            PM.onGeyser = true;

            jumpingFromGeyser = true;
        }

        if (c.gameObject.tag == "Hazard")
        {
            Player_Stats.TakeDamage(10);
        }

        if (c.gameObject.tag == "HazardRock" && PM.takeFallDamage)
        {
            if (transform.position.y <= exitPosY - PM.fallDamageHeight)
            {
                Player_Stats.TakeDamage(30);
                Player_Movement.grounded = true;

                Debug.Log(gameObject.name + ": Player_Collision, Player should take fall damage here from Hazardous Rock...");
            }
        }

        // Call Mini-Game script when interacting with mineral
        if (c.gameObject.tag == "Aragonite" || c.gameObject.tag == "Feldspar" || c.gameObject.tag == "Random")
        {
            GUI_MineralAnalysis.currentMineral = c.gameObject.tag;
            //InfoPanel.AragoniteText();

            GUI_HUD.staticPrompt.gameObject.SetActive(true);
            GUI_HUD.staticPrompt.text = "Press [E] to analyze with the PIXL camera...";
        }

        if(c.gameObject.tag == "RIMFAX")
        {
            GUI_HUD.staticPrompt.gameObject.SetActive(true);
            GUI_HUD.staticPrompt.text = "Press [E] to scan underground with RIMFAX...";
        }

        if(c.gameObject.tag == "Drill")
        {
            GUI_HUD.staticPrompt.gameObject.SetActive(true);
            GUI_HUD.staticPrompt.text = "Press [E] to DRILL for sample...";
        }

        if(c.gameObject.tag == "SafeZone")
        {
            //InfoPanel.CheckPointText();
            GUI_HUD.staticPrompt.gameObject.SetActive(true);
            GUI_HUD.staticPrompt.text = "Press [E] to set Safe Zone...";
            InfoPanel.CheckPointMessage();
        }

        if (c.gameObject.tag == "DustNotification")
        {
            InfoPanel.DustDevilNotification();
        }

        if (c.gameObject.CompareTag("FactTrigger"))
        {
            InfoPanel.GenerateFact();
            InfoPanel.ActivateFactPanel();
         //   c.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider c)
    {
        // Call Mini-Game script when interacting with mineral
        if (c.gameObject.tag == "Aragonite" || c.gameObject.tag == "Feldspar" || c.gameObject.tag == "Random")
        {
            // Open Mini-Game
            if (Input.GetKeyDown(KeyCode.E))
            {
                MiniGame.MiniGame_PIXL();
                GUI_HUD.staticPrompt.gameObject.SetActive(false);
                c.gameObject.SetActive(false);
            }
        }

        if (c.gameObject.tag == "RIMFAX")
        {
            // Open Mini-Game
            if (Input.GetKeyDown(KeyCode.E))
            {
                MiniGame.MiniGame_RIMFAX();
                GUI_HUD.staticPrompt.gameObject.SetActive(false);
                c.gameObject.SetActive(false);
            }
        }

        if(c.gameObject.tag == "Drill")
        {
            // Open Mini-Game
            if (Input.GetKeyDown(KeyCode.E))
            {
                MiniGame.MiniGame_DRILL();
                GUI_HUD.staticPrompt.gameObject.SetActive(false);
                c.gameObject.SetActive(false);
            }
        }

        if (c.gameObject.tag == "Geyser")
        {
            //PM.jumpHeight = PM.geyserJumpHeight;
            //PM._CCMovement.y = PM._CCMovement.y + 10f;
            Player_Movement.grounded = true;
            PM.onGeyser = true;
            jumpingFromGeyser = true;
        }
    }

    private void OnTriggerExit(Collider c)
    { 
        // Reset jump to standard after leaving geyser
        
        if (c.gameObject.tag == "Geyser")
        {
            //PM.jumpHeight = 0.5f;
            PM.onGeyser = false;
            Player_Movement.grounded = false;
        }

        // Disable prompt after leaving mineral
        if (c.gameObject.tag == "Aragonite" || c.gameObject.tag == "Feldspar" || c.gameObject.tag == "Random" || c.gameObject.tag == "RIMFAX")
        {
            GUI_HUD.staticPrompt.gameObject.SetActive(false);
        }

        if(c.gameObject.tag == "SafeZone")
        {
            GUI_HUD.staticPrompt.gameObject.SetActive(false);
        }
    }
}
