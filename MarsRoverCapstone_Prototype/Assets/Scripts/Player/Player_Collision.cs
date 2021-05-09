using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    Player_Movement _Player_Movement => GetComponent<Player_Movement>();
    Rigidbody RB => GetComponent<Rigidbody>();

    private void Update()
    {
        // Coyote Time: Allow player to press jump button a few frames after leaving ground
        if(Time.time > Player_Movement.coyoteTime)
        {
            Player_Movement.grounded = false;
        }
    }

    // COLLISION Detection 
    private void OnCollisionEnter(Collision c)
    {

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

        }
    }
}
