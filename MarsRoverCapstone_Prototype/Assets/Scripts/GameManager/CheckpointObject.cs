using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour
{
    private GM_Checkpoint GM => GameObject.FindObjectOfType<GameManager>().GetComponent<GM_Checkpoint>();

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
            GM.lastCheckpoint = collision.transform;

            Debug.Log(gameObject.name + ": Set Checkpoint to " + GM.lastCheckpoint.position);
        }
    }
}
