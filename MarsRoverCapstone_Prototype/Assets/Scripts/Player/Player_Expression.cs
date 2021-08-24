using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Expression : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * Player Expression
     * This script handles aesthetic motions or expression that the rover is capable of.
     */

    public Light headlight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && headlight.enabled == true)
        {
            headlight.enabled = false;
        }
        else if(Input.GetKeyDown(KeyCode.F) && headlight.enabled == false)
        {
            headlight.enabled = true;
        }
    }
}
