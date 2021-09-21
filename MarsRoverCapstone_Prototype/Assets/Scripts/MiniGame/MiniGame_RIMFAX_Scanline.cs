using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_RIMFAX_Scanline : MonoBehaviour
{
    public MiniGame_RIMFAX parent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "RIMFAX")
        {
            //Debug.Log("Scanline touched scanning point");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    { 
        if (other.gameObject.tag == "RIMFAX" && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Destroy(other.gameObject);
            parent.scanninglinesCounter++;
        }

        if (other.gameObject.tag != "RIMFAX" && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            parent.Fail();
        }
    }
}
