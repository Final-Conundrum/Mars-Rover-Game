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

    // Speed variables, the range between min and max speed is -1 to 1
    public float driveSpeed = 0.03f;
    public float rotateSpeed = 1f;
    public float jumpSpeed = 400f;

    Rigidbody body => GetComponent<Rigidbody>();
    BoxCollider col => GetComponent<BoxCollider>();

    public bool grounded;

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
        
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            body.AddForce(transform.up * jumpSpeed);
        }

        transform.Translate(0, 0, acceleration);
        transform.Rotate(0, rotation, 0);
    }

    private void OnCollisionEnter(Collision c)
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
