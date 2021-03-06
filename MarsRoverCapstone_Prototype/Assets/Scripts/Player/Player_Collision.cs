using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    Player_Movement PM => GetComponent<Player_Movement>();
    Rigidbody RB => GetComponent<Rigidbody>();
    MiniGame_Systems MiniGame => FindObjectOfType<MiniGame_Systems>();
    GUI_infoPanel InfoPanel => FindObjectOfType<GUI_infoPanel>();

    private float exitPosY;
    private bool inDamageZone = false;



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
        if (c.gameObject.tag == "Ground" && PM.takeFallDamage)
        {
            if(transform.position.y <= exitPosY - PM.fallDamageHeight)
            {
                Player_Stats.TakeDamage(15);
                Player_Movement.grounded = true;

                Debug.Log(gameObject.name + ": Player_Collision, Player should take fall damage here...");
            }
        }

        if (c.gameObject.tag == "Ground")
        {
            PM.onGeyser = false;
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
            PM.onGeyser = true;
        }

        if (c.gameObject.tag == "Hazard")
        {
            Player_Stats.TakeDamage(5);
            inDamageZone = true;
            StartCoroutine(DamageOverTime());

        }

        if (c.gameObject.tag == "HazardRock")
        {

            Player_Stats.TakeDamage(5);
            Player_Movement.grounded = true;

            Debug.Log(gameObject.name + ": Player_Collision, Player should take fall damage here from Hazardous Rock...");
            
        }

        // Call Mini-Game script when interacting with mineral
        if (c.gameObject.tag == "PIXLMineral")
        {
            GUI_MineralAnalysis.currentMineral = c.gameObject.tag;

            GUI_HUD.UpdatePrompt("E", "Analyze with the PIXL camera", true);
        }

        if(c.gameObject.tag == "RIMFAX")
        {
            GUI_HUD.UpdatePrompt("E", "Scan underground with RIMFAX", true);
        }

        if (c.gameObject.tag == "Drill")
        {
            GUI_HUD.UpdatePrompt("E", "DRILL for sample", true);
        }

        if (c.gameObject.tag == "DustNotification")
        {
            InfoPanel.DustDevilNotification();
        }

        if (c.gameObject.CompareTag("FactTrigger"))
        {
            InfoPanel.GenerateFact();
            InfoPanel.ActivateFactPanel();
         // c.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider c)
    {
        // Call Mini-Game script when interacting with mineral
        if (c.gameObject.tag == "PIXLMineral")
        {
            // Open Mini-Game
            if (Input.GetKeyDown(KeyCode.E))
            {
                MiniGame.MiniGame_PIXL();
                GUI_HUD.UpdatePrompt("", "", false);
                c.gameObject.GetComponent<MiniGame_PIXLMineral>().CompleteThis();
                c.gameObject.SetActive(false);
            }
        }

        if (c.gameObject.tag == "RIMFAX")
        {
            // Open Mini-Game
            if (Input.GetKeyDown(KeyCode.E))
            {
                MiniGame.MiniGame_RIMFAX();
                GUI_HUD.UpdatePrompt("", "", false);
                c.gameObject.GetComponent<MiniGame_RIMFAXLocation>().CompleteThis();
                c.gameObject.SetActive(false);
            }
        }

        if(c.gameObject.tag == "Drill")
        {
            // Open Mini-Game
            if (Input.GetKeyDown(KeyCode.E))
            {
                MiniGame.MiniGame_DRILL();
                GUI_HUD.UpdatePrompt("", "", false);
                c.gameObject.GetComponent<MiniGame_DrillLocation>().CompleteThis();
                c.gameObject.SetActive(false);
            }
        }

        if (c.gameObject.tag == "Geyser")
        {
            Player_Movement.grounded = true;
            PM.onGeyser = true;
        }

        if (c.gameObject.tag == "Hazard")
        {
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if(c.gameObject.tag == "Hazard")
        {
            inDamageZone = false;
        }

        // Reset jump to standard after leaving geyser

        if (c.gameObject.tag == "Geyser")
        {
            StartCoroutine(ExittingGeyser());
        }

        // Disable prompt after leaving mineral
        if (c.gameObject.tag == "PIXLMineral" || c.gameObject.tag == "RIMFAX" || c.gameObject.tag == "Drill" || c.gameObject.tag == "SafeZone")
        {
            GUI_HUD.UpdatePrompt("", "", false);
        }

        if(c.gameObject.tag == "FactTrigger")
        {
            c.gameObject.SetActive(false);
        }
    }

    IEnumerator ExittingGeyser()
    {
        yield return new WaitForSeconds(2f);

        PM.onGeyser = false;
        Player_Movement.grounded = false;
    }

    IEnumerator DamageOverTime()
    {
        while(inDamageZone)
        {
            yield return new WaitForSeconds(2f);
            Player_Stats.TakeDamage(5);
        }
    }
}
