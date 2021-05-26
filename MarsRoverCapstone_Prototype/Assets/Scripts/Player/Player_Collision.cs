using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    Player_Movement _Player_Movement => GetComponent<Player_Movement>();
    //Player_Stats _Player_Stats => GetComponent<Player_Stats>();
    Rigidbody RB => GetComponent<Rigidbody>();

    [SerializeField] private float exitPosY;

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
        if (c.gameObject.tag == "Ground" && _Player_Movement.takeFallDamage)
        {
            if(transform.position.y <= exitPosY - _Player_Movement.fallDamageHeight)
            {
                //I put 10 here as I changed the TakeDamage method to take in a damage value and no fall damage amount has been set. :)
                Player_Stats.TakeDamage(30);
                Player_Movement.grounded = true;

                Debug.Log(gameObject.name + ": Player_Collision, Player should take fall damage here...");
            }
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
            Player_Movement.coyoteTime = Time.time + _Player_Movement._coyoteTime;
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
            _Player_Movement.RB.velocity += transform.up * _Player_Movement.geyserJumpHeight;
        }

        if (c.gameObject.tag == "Hazard")
        {
            Player_Stats.TakeDamage(10);

        }

        if(c.gameObject.tag == "Aragonite" || c.gameObject.tag == "Feldspar")
        {
            GUI_MineralAnalysis.currentMineral = c.gameObject.tag;
            MiniGame_Systems.MiniGame_PIXL();
        }
        
   }

    }

