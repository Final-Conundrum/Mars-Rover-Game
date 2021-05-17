using System.Collections;
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

    public Rigidbody RB => GetComponent<Rigidbody>();
    Player_RigidbodyMovement RBclass => GetComponent<Player_RigidbodyMovement>();
    CharacterController CC => GetComponent<CharacterController>();

    public static bool grounded;
    public bool _alignToGround = false;
    public static bool alignToGround;
    public bool tankControls = true;

    // Character Controller variables
    public Vector3 _CCMovement;
    public float gravity = 2f;

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

    // Jump variables, the Fall variables modify the speed in which the rover drops after the jump to give it weight
    [SerializeField] private bool _isJumping = false;
    public float jumpHeight = 0.5f;
    public float geyserJumpHeight = 1f;

    public float _coyoteTime = 0.2f;
    public static float coyoteTime;

    public float fallDamageHeight = 25f;
    public static float elevation;
    public bool takeFallDamage = false;

    // Slope variables
    public bool onSteepSlope = false;
    public float slopeGravityMuliplier;
    public float slopeCastRadius = 1f;
    public float slopeCastDistance = 1f;
    private Vector3 _slopeCastDirection;
    private RaycastHit _slopeCastHit;

    // Input variables
    private float _rotation;

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
        // Set if Rover should align to ground
        alignToGround = _alignToGround;

        // Rotation inputs for WASD and Arrow keys
        _rotation = Input.GetAxis("Horizontal") * _rotateSpeed;
       
        // Increase gravity while moving down slope for smooth incline 
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && OnSteepSlope())
        {
            CC.Move(Vector3.down * CC.height / 2 * slopeGravityMuliplier * Time.deltaTime);
        }

        roverSpeed = _currentSpeed;
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

        // Steep Slope Movement
        if (!OnSteepSlope())
        {
            LockConstraints(true);
        }
        else if (OnSteepSlope())
        {
            LockConstraints(false);
        }

        // The Rover is controlled by Tank controls (Forward/Back = Acceleration/Deceleration, Left/Right = Rotate Rover)
        switch (tankControls)
        {
            // Movement Setup and modifiers while on/off ground
            case true:
                switch (grounded)
                {
                    // Player is Grounded
                    case true:
                        _CCMovement.y = 0f;
                        
                        takeFallDamage = false;

                        // Input and AddForce for JUMP
                        if (Input.GetKey(KeyCode.Space))
                        {
                            _CCMovement.y = jumpHeight;
                            _isJumping = true;
                            grounded = false;
                        }

                        // Rotate Rover direction with input
                        transform.Rotate(0, _rotation, 0);

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

                        // Decrease Rotation and Movement speed
                        transform.Rotate(0, _rotation * airSpeedDivision, 0);

                        CCMovementControl(_currentSpeed * airSpeedDivision);
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

    // NON-TANK CONTROLS: Transform Rover in Right Axis direction based on camera angle.
    private void StandardMovementDirection(float verticalAxis, float horizontalAxis)
    {       

    }

    // NON-TANK CONTROLS: Rotate Rover in direction of movement automatically.
    private void StandardRotationDirection(float horizontalAxis)
    {

    }

    // Return if positioned on a slope
    private bool OnSteepSlope()
    {
        _slopeCastDirection = transform.forward;

        RaycastHit hit = new RaycastHit();
        Ray raycast = new Ray(transform.position, _slopeCastDirection);

        if (Physics.SphereCast(raycast, slopeCastRadius, out hit, slopeCastDistance))
        {
            Vector3 slope = hit.normal;
            _slopeCastHit = hit;

            if ((slope.x > 0.6f || slope.x < -0.6f || slope.z > 0.6f || slope.z < -0.6f) && !hit.collider.isTrigger)
            {
                onSteepSlope = true;
                return true;
            }
        }

        Ray raycast2 = new Ray(transform.position, -_slopeCastDirection);

        if (Physics.SphereCast(raycast2, slopeCastRadius, out hit, slopeCastDistance))
        {
            Vector3 slope = hit.normal;
            _slopeCastHit = hit;

            if (slope.x > 0.6f || slope.x < -0.6f || slope.z > 0.6f || slope.z < -0.6f)
            {
                onSteepSlope = true;
                return true;
            }
        }

        onSteepSlope = false;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(transform.position, transform.position + _slopeCastDirection * _slopeCastHit.distance);
        Gizmos.DrawWireSphere(transform.position + _slopeCastDirection * _slopeCastHit.distance, slopeCastRadius);
        Gizmos.DrawWireSphere(transform.position + -_slopeCastDirection * _slopeCastHit.distance, slopeCastRadius);
    }

    // Called by Player_Collision to apply Fall damage
    public void CheckFallDamage()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            float distanceToGround = hit.distance;
            elevation = distanceToGround;
            //Debug.Log("DistanceToGround: " + distanceToGround);

            if(distanceToGround > fallDamageHeight)
            {
                takeFallDamage = true;
            }
        }
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
}
