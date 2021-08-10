using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LookDirections : MonoBehaviour
{
    /* Rotates the parent of the Rover models Direction Spheres when Tank controls are disabled. 
     * Simulates the Rover turning in different directions.
     */
    Player_Movement PM => GetComponentInParent<Player_Movement>();
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler (0, PM.playerCam.transform.rotation.eulerAngles.y, 0);
    }
}
