using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

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
    [SerializeField] public Camera mainCamera => Camera.main;

    // Speed variables, the range between min and max speed is -1 to 1
    public float driveSpeed = 0.1f;
    public float airSpeedDivision = 0.5f;
    public float rotateSpeed = 1f;
    public float jumpSpeed = 400f;

    Rigidbody body => GetComponent<Rigidbody>();
    BoxCollider col => GetComponent<BoxCollider>();

    [SerializeField] private bool grounded;

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

        switch (grounded)
        {
            case true:
                // Acceleration of Rover
                body.velocity += transform.forward * acceleration;
                
                transform.Rotate(0, rotation, 0);

                // Input and AddForce for JUMP
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    body.AddForce(transform.up * jumpSpeed);
                }

                // ALTERNATE MOVEMENT: Use translate for acceleration
                //transform.Translate(0, 0, acceleration);
                break;

            case false:
                // Floats for mid-air translation
                float verticalAxis = Input.GetAxis("Vertical");
                float horizontalAxis = Input.GetAxis("Horizontal");

                //body.velocity += (transform.forward * verticalAxis * airSpeed) + AirMovementDirection(verticalAxis, horizontalAxis) * airSpeed;     

                //AirRotationDirection(horizontalAxis);

                // ALTERNATE MID-AIR ROTATIONS
                if(acceleration >= 0f)
                {
                    body.velocity += transform.forward * acceleration * airSpeedDivision;
                }
                transform.Rotate(0, rotation * 0.8f, 0);

                // ALTERNATE MID-AIR TRANSLATIONS
                //body.velocity += transform.right * horizontalAxis * airSpeed;
                //body.velocity += transform.forward * verticalAxis * airSpeed;
                //transform.Translate(horizontalAxis, 0, verticalAxis);
                break;
        }
    }

    // Move object in Right Axis direction based on camera angle
    private Vector3 AirMovementDirection(float verticalAxis, float horizontalAxis)
    {       
        Vector3 direction;
        //Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        //forward.y = 0f;
        right.y = 0f;

        //forward.Normalize();
        right.Normalize();

        //return direction = forward * verticalAxis + right * horizontalAxis;
        return direction = right * horizontalAxis;
    }

    private void AirRotationDirection(float horizontalAxis)
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
}
