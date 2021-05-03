using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    Rigidbody RB => GetComponent<Rigidbody>();

    // COLLISION Detection 
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
        }
    }
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
