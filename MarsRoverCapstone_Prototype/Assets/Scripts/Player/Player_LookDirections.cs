using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LookDirections : MonoBehaviour
{
    Player_Movement PM => GetComponentInParent<Player_Movement>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x, PM.playerCam.transform.rotation.y, transform.rotation.z, transform.rotation.w);

    }
}
