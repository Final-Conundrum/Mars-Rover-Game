using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Checkpoint : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * GM_Checkpoint: This script handles the saving of checkpoint locations to reload the player and setting up
     * the specific safe zone/checkpoint the player loads to.
     *  
     */

    public bool savedAtCheckpoint = false;
    public Vector3 lastCheckpoint;

    public CheckpointObject[] checkpoints;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
