using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Expression : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * Player Expression
     * This script handles aesthetic motions or expression that the rover is capable of.
     */

    public Light headlight;

    public CinemachineVirtualCamera[] _finaleCameras;
    public static CinemachineVirtualCamera[] finaleCameras;

    void Start()
    {
        finaleCameras = _finaleCameras;

        foreach(CinemachineVirtualCamera i in finaleCameras)
        {
            Debug.Log("Player_Expression: Added new finale camera");
        }
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
