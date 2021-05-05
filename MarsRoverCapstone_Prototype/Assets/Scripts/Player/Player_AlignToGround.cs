using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]

public class Player_AlignToGround : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * AlignToGround: 
     * Add this script to the child (Body) of the parent Player object so that it aligns the aesthetic Rover body to ground.
     */
    CapsuleCollider coll => GetComponent<CapsuleCollider>();
    Rigidbody RB => GetComponent<Rigidbody>();
    public float slopeRaycastDistance = 1.5f;

    private void Update()
    {
        // Correct rotation if it rotates too far
        transform.rotation = new Quaternion(transform.rotation.x, transform.parent.rotation.y, transform.rotation.z, transform.rotation.w);
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);

        if (transform.rotation.x > 50f)
        {
            transform.rotation = new Quaternion(50f, transform.parent.rotation.y, transform.rotation.z, transform.rotation.w);
        }

        if (transform.rotation.x > 50f)
        {
            transform.rotation = new Quaternion(-50f, transform.parent.rotation.y, transform.rotation.z, transform.rotation.w);
        }

        if (transform.rotation.z > 50f)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.parent.rotation.y, 50f, transform.rotation.w);
        }

        if (transform.rotation.z > 50f)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.parent.rotation.y, -50f, transform.rotation.w);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(Player_Movement.grounded)
        {
            case true:
                AlignToGround();
                RBCustomConstraints(true);
                break;
            case false:
                RBCustomConstraints(false);
                break;
        }     
    }

    // Rotate Rover to align with current ground
    private void AlignToGround()
    {
        RaycastHit hit = new RaycastHit();
        Ray raycast = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(raycast, out hit, slopeRaycastDistance))
        {
            Vector3 slope = hit.normal;
            Debug.DrawRay(transform.position, slope, Color.green);

            transform.rotation = Quaternion.FromToRotation(transform.up, slope.normalized) * transform.rotation;
        }
    }

    // Modify RigidBody rotation and position constraint. For the purpose of aligning Rover to ground slope. 
    // When the player is grounded, rotation x and z are unlocked so that the AlignToGround() code may function.
    // While in mid-air, all rotations are locked.
    private void RBCustomConstraints(bool grounded)
    {
        switch (grounded)
        {
            case true:
                RB.constraints = RigidbodyConstraints.FreezePosition;
                break;
            case false:
                RB.constraints = RigidbodyConstraints.FreezeAll;
                break;
        }
    }
}
