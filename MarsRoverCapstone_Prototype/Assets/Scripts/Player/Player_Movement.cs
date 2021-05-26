﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]

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

    public Rigidbody RB => GetComponent<Rigidbody>();
    CharacterController CC => GetComponent<CharacterController>();

    public static bool grounded;
    public bool _alignToGround = true;
    public bool tankControls = true;
    public Camera playerCam;

    [Space]

    // Character Controller variables
    [SerializeField] private Vector3 _CCMovement;
    public float gravity = 2f;

    [Space]
    [Header("Player Speed Values")]
    // Speed variables, the range between min and max speed is -1 to 1
    public float minDriveSpeed = 4f;
    public float midDriveSpeed = 9f;
    public float maxDriveSpeed = 15;

    public float momentumIncrease = 0.02f;
    public float airSpeedDivision = 0.5f;
    public float rotateSpeed = 1f;

    private float _rotateSpeed;
    [SerializeField] private float _currentSpeed;
    public static float roverSpeed;

    // Input variables
    private float _rotation;

    [Space]
    [Header("Jump and Midair Values")]
    // Jump variables, the Fall variables modify the speed in which the rover drops after the jump to give it weight
    [SerializeField] private bool _isJumping = false;
    public float jumpHeight = 0.5f;
    public float geyserJumpHeight = 1f;

    public float _coyoteTime = 0.2f;
    public static float coyoteTime;

    public float fallDamageHeight = 25f;
    public static float elevation;
    public bool takeFallDamage = false;

    [Space]
    [Header("On Slope Hit values")]
    // Slope variables
    [SerializeField] private bool onSteepSlope = false;
    [SerializeField] private Vector3 hitNormal;
    public float slideFriction = 0.3f;
    public float slideMuliplier = 0.3f;
    public float slideTimer = 0.9f;
    private float newSlideTimer;
    public Text TankControlsActive;


    // Start is called before the first frame update
    void Start()
    {
        // Freeze constraints so that Character Controller overrides phyhsics
        RB.constraints = RigidbodyConstraints.FreezeAll;

        _currentSpeed = minDriveSpeed;
        _rotateSpeed = rotateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation inputs for WASD and Arrow keys
        _rotation = Input.GetAxis("Horizontal") * _rotateSpeed;
        roverSpeed = _currentSpeed;

        if (!tankControls)
        {
            TankControlsActive.color = Color.red;
            TankControlsActive.text = "Tank Controls \n OFF";
        }
        else
        {
            TankControlsActive.color = Color.cyan;
            TankControlsActive.text = "Tank Controls \n ON";
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            if (!tankControls)
            {
                tankControls = true;
            }
            else if (tankControls)
            {
                tankControls = false;
            }
        }      
    }

    // FixedUpdate reserved for modifying physics
    private void FixedUpdate()
    {
        // CC Gravity
        _CCMovement.y -= gravity * Time.fixedDeltaTime;

        //Speed, Momentum and Shift speed boost
        if ((Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && _currentSpeed < maxDriveSpeed)
        {
            _currentSpeed += momentumIncrease;
        }
        else if((Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && _currentSpeed > midDriveSpeed)
        {
            _currentSpeed -= momentumIncrease;
        }
        else if((Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) && _currentSpeed < midDriveSpeed)
        {
            _currentSpeed += momentumIncrease;
        }
        else if(Input.GetAxis("Vertical") == 0 && _currentSpeed > minDriveSpeed)
        {
            _currentSpeed -= momentumIncrease;
        }

        RaycastHit hit = new RaycastHit();
        Ray raycastDown = new Ray(transform.position, -transform.up);

        if (Physics.SphereCast(raycastDown, 0.6f, out hit, transform.localScale.y / 2))
        {
            if(hit.collider.gameObject.tag == "Ground")
            {
                onSteepSlope = false;
                hitNormal = new Vector3(0, 1, 0);
                grounded = true;

            }
        }

        // Movement Setup and modifiers while on/off ground
        switch (grounded)
        {
            // Player is Grounded
            case true:
                _CCMovement.y = 0f;
                takeFallDamage = false;

                // Input and AddForce for JUMP
                if (Input.GetKey(KeyCode.Space) && !onSteepSlope)
                {
                    _CCMovement.y = jumpHeight;
                    _isJumping = true;
                    grounded = false;
                }

                if(tankControls)
                {
                    // Rotate Rover direction with input
                    transform.Rotate(0, _rotation, 0);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }

                // Finalize Movement
                CCMovementControl(_currentSpeed);
                break;

            // Player is Mid-air
            case false:
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

                // Check for fall damage
                CheckFallDamage();

                if(tankControls)
                {
                    // Decrease Rotation and Movement speed
                    transform.Rotate(0, _rotation * airSpeedDivision, 0);
                }

                CCMovementControl(_currentSpeed * airSpeedDivision);
                break;
        }
    }

    // Method to encompass getting input and using CC to move object
    private void CCMovementControl(float movementSpeed)
    {
        Vector3 inputDirection;

        // Control movement and direction
        switch (tankControls)
        {
            case true:
                inputDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                break;
            case false:
                inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                inputDirection = playerCam.transform.TransformDirection(inputDirection);
                inputDirection.y = 0.0f;
                break;
        }
        Vector3 transformDirection = transform.TransformDirection(inputDirection); ;
        Vector3 flatMovement = movementSpeed * Time.deltaTime * transformDirection;

        // Slide down slopes
        
        if (Vector3.Angle(Vector3.up, hitNormal) > CC.slopeLimit + 10)
        {
            newSlideTimer = Time.time + slideTimer;
            onSteepSlope = true;
        }
        else
        {
            onSteepSlope = false;
        }

        if (onSteepSlope)
        {
            _CCMovement.x += (1f - hitNormal.y) * hitNormal.x * (1f - slideFriction);
            _CCMovement.z += (1f - hitNormal.y) * hitNormal.z * (1f - slideFriction);

            _CCMovement.x *= slideMuliplier;
            _CCMovement.z *= slideMuliplier;
            flatMovement.x *= slideMuliplier;
            flatMovement.z *= slideMuliplier;

            _CCMovement = new Vector3(flatMovement.x + _CCMovement.x, _CCMovement.y, flatMovement.z + _CCMovement.z);
        }
        else
        {
            _CCMovement.x = 0f;
            _CCMovement.z = 0f;

            _CCMovement = new Vector3(flatMovement.x, _CCMovement.y, flatMovement.z);
        }
        CC.Move(_CCMovement);
    }

    // Called by Player_Collision to apply Fall damage
    public void CheckFallDamage()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            float distanceToGround = hit.distance;
            elevation = distanceToGround;

            if(distanceToGround > fallDamageHeight)
            {
                takeFallDamage = true;
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position -transform.up * 2f, 1f);
    }
    // CODE ON HAITUS

    /*
    // Return if positioned on a slope
    private bool OnSteepSlope()
    {
        _slopeCastDirection = transform.forward;

        RaycastHit hit = new RaycastHit();
        Ray raycastForward = new Ray(transform.position, _slopeCastDirection);
        Ray raycastBack = new Ray(transform.position, -_slopeCastDirection);

        if (Physics.SphereCast(raycastForward, slopeCastRadius, out hit, slopeCastDistance))
        {
            Vector3 slope = hit.normal;
            _slopeCastHit = hit;

            if ((slope.x > 0.6f || slope.x < -0.6f || slope.z > 0.6f || slope.z < -0.6f) && !hit.collider.isTrigger)
            {
                //onSteepSlope = true;
                return true;
            }
        }
        else if (Physics.SphereCast(raycastBack, slopeCastRadius, out hit, slopeCastDistance))
        {
            Vector3 slope = hit.normal;
            _slopeCastHit = hit;

            if (slope.x > 0.6f || slope.x < -0.6f || slope.z > 0.6f || slope.z < -0.6f)
            {
                //onSteepSlope = true;
                return true;
            }
        }

        //onSteepSlope = false;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(transform.position, transform.position + _slopeCastDirection * _slopeCastHit.distance);
        Gizmos.DrawWireSphere(transform.position + _slopeCastDirection * _slopeCastHit.distance, slopeCastRadius);
        Gizmos.DrawWireSphere(transform.position + -_slopeCastDirection * _slopeCastHit.distance, slopeCastRadius);
    }


    // Lock Rigidbody constraints while using Character Controller. Only turned false when sliding down slope.
    public void LockConstraints(bool lockedConstraints)
    {
        switch(lockedConstraints)
        {
            case true:
                // Freeze constraints so that Character Controller overrides phyhsics

                CC.enabled = true;
                RBclass.enabled = false;
                RB.constraints = RigidbodyConstraints.FreezeAll;
                RB.useGravity = false;

                break;
            case false:

                CC.enabled = false;
                RBclass.enabled = true;
                RB.constraints = RigidbodyConstraints.FreezeRotation;
                RB.useGravity = true;
                break;
        }
    }
    */
}
