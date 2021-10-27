using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    Player_Collision PC => GetComponent<Player_Collision>();

    public static bool grounded;
    public bool _alignToGround = true;
    public bool tankControls = true;
    public Camera playerCam;
    public AudioSource audio_DriveSFX;
    public AudioSource audio_JumpSFX;
    public ParticleSystem boostParticles;
    public ParticleSystem steepSlopeParticles;

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

    public float boostLimit = 5f;
    public float boostConsumptionRate;
    public float boost;

    private float _rotateSpeed;
    [SerializeField] private float _currentSpeed;
    public static float roverSpeed;

    // Input variables
    private float _rotation;

    [Space]
    [Header("Jump and Midair Values")]
    // Jump variables, the Fall variables modify the speed in which the rover drops after the jump to give it weight
    public float jumpHeight = 0.5f;
    public float geyserBurstElevation = 0.15f;
    public bool onGeyser = false;

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
    public TMP_Text realisticControlsStatus;

    // Start is called before the first frame update
    void Start()
    {
        // Freeze constraints so that Character Controller overrides phyhsics
        RB.constraints = RigidbodyConstraints.FreezeAll;

        _currentSpeed = minDriveSpeed;
        _rotateSpeed = rotateSpeed;

        boost = boostLimit;

        boostParticles.gameObject.SetActive(false);
        steepSlopeParticles.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation inputs for WASD and Arrow keys
        _rotation = Input.GetAxis("Horizontal") * _rotateSpeed;
        roverSpeed = _currentSpeed;

        // Tank control UI element
        if (!tankControls)
        {
            realisticControlsStatus.color = Color.white;
            realisticControlsStatus.text = "Realistic Controls \n OFF";
        }
        else
        {
            realisticControlsStatus.color = Color.yellow;
            realisticControlsStatus.text = "Realistic Controls \n ON";
        }

        
        if(Input.GetKeyDown(KeyCode.T))
        {
            SwapControlType();
        }

        // Control particle effect status
        if(_currentSpeed > midDriveSpeed + 1)
        {
            boostParticles.gameObject.SetActive(true);
        }
        else if(_currentSpeed <= midDriveSpeed )
        {
            boostParticles.gameObject.SetActive(false);
        }

        if(onSteepSlope)
        {
            steepSlopeParticles.gameObject.SetActive(true);
        }
        else
        {
            steepSlopeParticles.gameObject.SetActive(false);
        }
    }

    // FixedUpdate reserved for modifying physics
    private void FixedUpdate()
    {
        // CC Gravity
        _CCMovement.y -= gravity * Time.fixedDeltaTime;

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            BoostSpeed(true);
        }
        else
        {
            BoostSpeed(false);
        }

        // Raycast whether play is on flat ground
        RaycastHit hit = new RaycastHit();
        Ray raycastDown = new Ray(transform.position, -transform.up);

        if (Physics.SphereCast(raycastDown, 0.7f, out hit, transform.localScale.y / 2))
        {
            if(hit.collider.gameObject.tag == "Ground")
            {
                onSteepSlope = false;
                hitNormal = new Vector3(0, 1, 0);
                grounded = true;
                audio_DriveSFX.Play();
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
                if(!onSteepSlope)
                {        
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Jump();
                    }
                    
                    if (Input.GetKeyDown(KeyCode.Space) && !MiniGame_Systems.playingMinigame)
                    {
                        audio_JumpSFX.Play();
                    }
                }

                // Transform player upwards while within a geyser burst
                if(onGeyser)
                {
                    _CCMovement.y += geyserBurstElevation;
                }

                // Modify passed values for movement depending on active state of tank controls
                if (tankControls)
                {
                    // Rotate Rover direction with input
                    transform.Rotate(0, _rotation, 0);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }

                // Finalize Movement values for grounded state
                CCMovementControl(_currentSpeed);
                break;

            // Player is Mid-air
            case false:
                if (_CCMovement.y > (jumpHeight / 1.5) && !Input.GetKey(KeyCode.Space))

                {
                    _CCMovement.y = 0f;
                }

                
                // Audio mid-air
                audio_DriveSFX.Pause();

                if(Input.GetKeyUp(KeyCode.Space))
                {
                    audio_JumpSFX.Stop();
                }

                // Check for fall damage
                CheckFallDamage();

                if(tankControls)
                {
                    // Decrease Rotation and Movement speed
                    transform.Rotate(0, _rotation * airSpeedDivision, 0);
                }

                // Finalize Movement values for airborn state
                CCMovementControl(_currentSpeed * airSpeedDivision);
                break;
        }
        // Driving SFX based on rovers speed
        audio_DriveSFX.pitch = _currentSpeed / 10;
    }

    // Method to encompass getting input and using CC to move object
    private void CCMovementControl(float movementSpeed)
    {
        Vector3 inputDirection;

        // Control movement and direction depending on tank controls or not
        switch (tankControls)
        {
            case true:
                // Acceleration/deceleration of forward/back keys
                inputDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                break;
            case false:
                // Allowing control from all four direction keys with Axis based on camera direction.
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
        
        // Translate object opposite to current slope
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

        // Interrupt movement operations if playing minigame
        if(!MiniGame_Systems.playingMinigame || !GM_Objectives.endOfGame)
        {
            // Finalize Movement directions
            CC.Move(_CCMovement);
        }
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

    public void Jump()
    {
        if (!onSteepSlope || !GM_Objectives.endOfGame)
        {
            _CCMovement.y = jumpHeight;

            grounded = false;
        }
    }

    public void SwapControlType()
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

    public void BoostSpeed(bool currentlyBoosting)
    {
        //Speed, Momentum and Shift speed boost
        if ((Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) && currentlyBoosting)
        {
            if (boost > 0.5f && _currentSpeed < maxDriveSpeed)
            {
                _currentSpeed += momentumIncrease;
            }
            else if (boost <= 0.5f && _currentSpeed > midDriveSpeed)
            {
                _currentSpeed -= momentumIncrease;
            }
        }
        else if ((Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) && !currentlyBoosting && _currentSpeed > midDriveSpeed)
        {
            _currentSpeed -= momentumIncrease;
        }
        else if ((Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) && _currentSpeed < midDriveSpeed)
        {
            _currentSpeed += momentumIncrease;
        }
        else if (Input.GetAxis("Vertical") == 0 && _currentSpeed > minDriveSpeed)
        {
            _currentSpeed -= momentumIncrease;
        }

        // Boost variables
        if (boost >= 0.5f && currentlyBoosting)
        {
            boost -= boostConsumptionRate;
        }
        else if (boost < boostLimit && !currentlyBoosting)
        {
            boost += boostConsumptionRate;
        }
    }

    // Get Normal vector of colliding surface
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }
}
