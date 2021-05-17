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

    private bool tankControls = true;
    private float maxSpeed = 20f;

    // Speed variables, the range between min and max speed is -1 to 1
    private float driveSpeed = 0.1f;
    private float airSpeedDivision = 0.5f;
    private float rotateSpeed = 1f;

    // Input variables
    private float _acceleration = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Movement inputs for WASD and Arrow keys trigger continuous translation
        _acceleration = Input.GetAxis("Vertical") * driveSpeed;
    }

    // FixedUpdate reserved for modifying physics
    private void FixedUpdate()
    {
        body.velocity += transform.forward * _acceleration;
    }
}
