﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]

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

    Rigidbody RB => GetComponent<Rigidbody>();
    CharacterController CC => GetComponent<CharacterController>();
    BoxCollider coll => GetComponent<BoxCollider>();

    public static bool grounded;
    public bool _alignToGround = false;
    public static bool alignToGround;
    public bool tankControls = true;

    // Character Controller variables
    private Vector3 _CCMovement;
    private Vector3 _currentGround;
    public float gravity = 2f;

    // Speed variables, the range between min and max speed is -1 to 1
    public float driveSpeed = 0.1f;
    public float airSpeedDivision = 0.5f;
    public float rotateSpeed = 1f;

    // Jump variables, the Fall variables modify the speed in which the rover drops after the jump to give it weight
    [SerializeField] private bool _isJumping = false;
    public float jumpHeight = 4f;
    private float _currentJump;
    private float _jumpRotation;

    private string _diffInY;
    private float _lastYPos;

    // Slope variables
    public bool onSlope = false;
    public float slopeRaycastDistance;
    public float slopeGravityMuliplier;
    public float slopeMaxX = 20f;
    public float slopeMaxZ = 20f;

    // Input variables
    private float _acceleration;
    private float _rotation;

    // Start is called before the first frame update
    void Start()
    {
        // Freeze constraints so that Character Controller overrides phyhsics
        RB.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        // Set if Rover should align to ground
        alignToGround = _alignToGround;

        // Movement inputs for WASD and Arrow keys
        _acceleration = Input.GetAxis("Vertical") * driveSpeed;
        _rotation = Input.GetAxis("Horizontal") * rotateSpeed;

        
        // Increase gravity while moving down slope for smooth incline 
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && OnSlope())
        {
            CC.Move(Vector3.down * CC.height / 2 * slopeGravityMuliplier * Time.deltaTime);
        }
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
                    // Player is Grounded
                    case true:
                        _CCMovement.y = 0f;

                        // Input and AddForce for JUMP
                        if (Input.GetKey(KeyCode.Space))
                        {
                            _CCMovement.y = jumpHeight;
                            _isJumping = true;
                        }

                        // Rotate Rover direction with input
                        transform.Rotate(0, _rotation, 0);

                        // Finalize Movement
                        CCMovementControl(driveSpeed);
                        break;

                    // Player is Mid-air
                    case false:
                        // CC Gravity
                        _CCMovement.y -= gravity * Time.deltaTime;

                        // Stop jump velocity after letting go jump button, giving it weighted feeling
                        if (_CCMovement.y > (jumpHeight / 2) && !Input.GetKey(KeyCode.Space))
                        {
                            _CCMovement.y = 0f;
                            _isJumping = false;

                        }
                        else if(!Input.GetKey(KeyCode.Space))
                        {
                            _isJumping = false;
                        }
                        else if(_CCMovement.y == jumpHeight)
                        {
                            _isJumping = false;
                        }

                        // Decrease Rotation and Movement speed
                        transform.Rotate(0, _rotation * airSpeedDivision, 0);

                        // Finalize Movement
                        if(Input.GetAxis("Vertical") >= 0)
                        {
                            CCMovementControl(driveSpeed * airSpeedDivision);
                        }
                        else
                        {
                            CCMovementControl(0.5f);
                        }
                        break;
                }                
                break;

            // Standard Character controls (Forward/Back = Transform Forward/Backward, Left/Right = Move Left, Move Right)
            case false:

                break;
        }
    }

    // Method to encompass getting input and using CC to move object
    private void CCMovementControl(float movementSpeed)
    {
        // Control movement and direction
        Vector3 inputDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        Vector3 transformDirection = transform.TransformDirection(inputDirection);

        Vector3 flatMovement = movementSpeed * Time.deltaTime * transformDirection;

        _CCMovement = new Vector3(flatMovement.x, _CCMovement.y, flatMovement.z);

        CC.Move(_CCMovement);
    }

    // Track if Y position changes
    private string DifferenceInY()
    {
        if (transform.position.y < _lastYPos - 1f)
        {
            _lastYPos = transform.position.y;
            return "Decreased";
        }
        else if (transform.position.y > _lastYPos + 1f)
        {
            _lastYPos = transform.position.y;
            return "Increased";
        }
        return "None";
    }

    // Return if positioned on a slope
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, CC.height / 2 * slopeRaycastDistance))
        {
            if (hit.normal != Vector3.up)
            {
                onSlope = true;
                return true;
            }             
        }
        return false;
    }

    // NON-TANK CONTROLS: Transform Rover in Right Axis direction based on camera angle.
    private void StandardMovementDirection(float verticalAxis, float horizontalAxis)
    {       

    }

    // NON-TANK CONTROLS: Rotate Rover in direction of movement automatically.
    private void StandardRotationDirection(float horizontalAxis)
    {

    }

    // Modify RigidBody rotation and position constraint.  
    private void RBCustomConstraints(bool lockRotation)
    {
        switch (grounded)
        {
            case true:
                RB.constraints = RigidbodyConstraints.FreezeAll;
                break;
            case false:
                RB.constraints = RigidbodyConstraints.FreezePosition;
                break;
        }
    }
}
