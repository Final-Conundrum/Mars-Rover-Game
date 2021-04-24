using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Player_Movement : MonoBehaviour
{
    /* EDITED BY: Dallas.
     * 
     * PLAYER MOVEMENT:
     * Attach to Player object.
     * Recieves inputs to transform the position/rotation of the object it is attached to.
     * 
     * The movement is designed after 'tank' controls, where players can accelerate, reverse 
     * and rotate their vehicle to drive in a different direction.
     */

    // Speed variables, the range between min and max speed is -1 to 1
    public float driveSpeed = 0.03f;
    public float rotateSpeed = 1f;
    public float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Movement inputs for WASD and Arrow keys trigger continuous translation
        float acceleration = Input.GetAxis("Vertical") * driveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotateSpeed;

        transform.Translate(0, 0, acceleration);
        transform.Rotate(0, rotation, 0);
    }

    // Method for jump
    private void Jump() 
    { 

    }
}
