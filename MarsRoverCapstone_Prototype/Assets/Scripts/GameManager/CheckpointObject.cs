using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * CheckpointObject: Attached to the trigger for the safe zone/checkpoint.
     * This is called by GM_Checkpoint to set this safe zone with the appropriate data.
     * 
     * i.e: inventory storage locker. 
     */

    private GM_Checkpoint GM => FindObjectOfType<GM_Checkpoint>();

    public Material untriggeredCheckpoint;
    public Material triggeredCheckedPoint;
    private MeshRenderer mesh => GetComponent<MeshRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        mesh.material = untriggeredCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mesh.material = triggeredCheckedPoint;
            GM.lastCheckpoint = transform.position;

            GM.savedAtCheckpoint = true;    

            Debug.Log(gameObject.name + ": Set Checkpoint to " + GM.lastCheckpoint);
        }
    }
}
