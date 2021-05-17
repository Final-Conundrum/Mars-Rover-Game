using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = GM_Checkpoint.lastCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
