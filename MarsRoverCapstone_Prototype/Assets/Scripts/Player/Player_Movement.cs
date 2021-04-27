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
    [SerializeField] private Camera mainCamera => Camera.main;
    [SerializeField] private bool grounded;
    
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
        switch (tankControls)
        {
            // The Rover is controlled by Tank controls (Forward/Back = Acceleration/Deceleration, Left/Right = Rotate Rover)
            case true:
                switch (grounded)
                {
                    case true:
                        // Acceleration of Rover
                        body.velocity += transform.forward * _acceleration;

                        // Rotate Rover
                        transform.Rotate(0, _rotation, 0);

                        // Input and AddForce for JUMP
                        if (Input.GetKey(KeyCode.Space))
                        {
                            body.AddForce(transform.up * jumpVelocity);
                        }
                        break;

                    case false:
                        // Modify speed while mid-air, while mid-air, only forward inputs apply to speed
                        if (_acceleration >= 0f)
                        {
                            body.velocity += transform.forward * (_acceleration * airSpeedDivision);
                        }

                        // Decrease Rotation speed
                        transform.Rotate(0, _rotation * airSpeedDivision, 0);

                        // Stop jump velocity after force, giving it weighted feeling
                        if (body.velocity.y < 0)
                        {
                            body.velocity += transform.up * Physics.gravity.y * (highJumpFall - 1) * Time.deltaTime;
                        }
                        else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
                        {
                            body.velocity += transform.up * Physics.gravity.y * (lowJumpFall - 1) * Time.deltaTime;
                        }
                        break;
                }
                break;

            // Standard Character controls (Forward/Back = Transform Forward/Backward, Left/Right = Move Left, Move Right(
            case false:
                // RigidBody's velocity moves on axis based on angle of camera.
                body.velocity += (transform.forward * _acceleration) + StandardMovementDirection(_acceleration, _rotation);

                // Rotate the Rover automatically in direction of movement
                StandardRotationDirection(_rotation);
                break;
        }


        // Cap the speed at MaxSpeed
        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
    }

    // NON-TANK CONTROLS: Transform Rover in Right Axis direction based on camera angle.
    private Vector3 StandardMovementDirection(float verticalAxis, float horizontalAxis)
    {       
        Vector3 direction;
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return direction = forward * verticalAxis + right * horizontalAxis;
    }

    // NON-TANK CONTROLS: Rotate Rover in direction of movement automatically.
    private void StandardRotationDirection(float horizontalAxis)
    {
        Vector3 right = mainCamera.transform.right;
        right.y = 0f;
        right.Normalize();

        if (horizontalAxis > 0f)
        {
            Vector3 rotateTarget = right - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateTarget, rotateSpeed / 2, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if (horizontalAxis < 1f)
        {
            Vector3 rotateTarget = right - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateTarget, rotateSpeed / 2, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    // COLLISION Detection
    private void OnCollisionStay(Collision c)
    {
        if(c.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }


    // ALTERNATE MOVEMENT EXPERIMENTATION

    // ALTERNATE MOVEMENT: Use translate for _acceleration
    //transform.Translate(0, 0, _acceleration);

    // ALTERNATE MID-AIR TRANSLATIONS
    //body.velocity += (transform.forward * verticalAxis * airSpeed) + AirMovementDirection(verticalAxis, horizontalAxis) * airSpeed;     
    //AirRotationDirection(horizontalAxis);
    //body.velocity += transform.right * horizontalAxis * airSpeed;
    //body.velocity += transform.forward * verticalAxis * airSpeed;
    //transform.Translate(horizontalAxis, 0, verticalAxis);
}
