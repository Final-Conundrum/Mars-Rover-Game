using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RigidbodyMovement : MonoBehaviour
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
    Rigidbody body => GetComponent<Rigidbody>();

    public bool tankControls = true;
    public float maxSpeed = 20f;

    // Speed variables, the range between min and max speed is -1 to 1
    public float driveSpeed = 0.1f;
    public float airSpeedDivision = 0.5f;
    public float rotateSpeed = 1f;

    // Jump variables, the Fall variables modify the speed in which the rover drops after the jump to give it weight
    public float jumpVelocity = 400f;
    public float highJumpFall = 2f;
    public float lowJumpFall = 1f;

    // Input variables
    private float _acceleration;
    private float _rotation;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement inputs for WASD and Arrow keys trigger continuous translation
        _acceleration = Input.GetAxis("Vertical") * driveSpeed;
        _rotation = Input.GetAxis("Horizontal") * rotateSpeed;
    }

    // FixedUpdate reserved for modifying physics
    private void FixedUpdate()
    {
        // The Rover is controlled by Tank controls (Forward/Back = Acceleration/Deceleration, Left/Right = Rotate Rover)
            switch (Player_Movement.grounded)
            {
                case true:
                    // Acceleration of Rover
                    body.velocity += transform.forward * _acceleration;
                    break;

                case false:
                    // Modify speed while mid-air, while mid-air, only forward inputs apply to speed
                    if (_acceleration >= 0f)
                    {
                        body.velocity += transform.forward * (_acceleration * airSpeedDivision);
                    }
                    break;
            }
        
        // Cap the speed at MaxSpeed
        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
    }
}
