using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    // COLLISION Detection 
    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
            Player_Movement.grounded = true;
        }
    }

    private void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
            Player_Movement.grounded = false;
        }
    }
}
