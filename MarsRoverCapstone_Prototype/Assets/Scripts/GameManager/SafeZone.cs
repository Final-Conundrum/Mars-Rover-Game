using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
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

    public void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mesh.material = triggeredCheckedPoint;
        }
    }
}
