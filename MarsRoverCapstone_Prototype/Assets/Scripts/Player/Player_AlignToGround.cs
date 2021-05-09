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

    public float slopeRaycastDistance = 1.5f;

    private void Update()
    {
        // Correct rotation if it rotates too far
        transform.rotation = new Quaternion(transform.rotation.x, transform.parent.rotation.y, transform.rotation.z, transform.rotation.w);
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);

        if (transform.rotation.x > 50f)
        {
            transform.rotation = new Quaternion(50f, transform.parent.rotation.y, 0, transform.rotation.w);
        }

        if (transform.rotation.x > 50f)
        {
            transform.rotation = new Quaternion(-50f, transform.parent.rotation.y, 0, transform.rotation.w);
        }

        if (transform.rotation.z > 50f)
        {
            transform.rotation = new Quaternion(0, transform.parent.rotation.y, 50f, transform.rotation.w);
        }

        if (transform.rotation.z > 50f)
        {
            transform.rotation = new Quaternion(0, transform.parent.rotation.y, -50f, transform.rotation.w);
        }

        // Finalize and rotate to Grounds Normal vector
        if(Player_Movement.alignToGround)
        {
            AlignToGround();
        }
    }

    void FixedUpdate()
    {
        
    }

    // Rotate Rover to align with current ground
    private void AlignToGround()
    {
        RaycastHit hit = new RaycastHit();
        Ray raycast = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(raycast, out hit, slopeRaycastDistance))
        {
            Vector3 slope = hit.normal;


            // Check if slopes normal vector is too steep
            if (!(slope.x > 0.7f || slope.x < -0.7f || slope.z > 0.7f || slope.z < -0.7f))
            {
                // Rotate to normals vector
                transform.rotation = Quaternion.FromToRotation(transform.up, slope.normalized) * transform.rotation;
            }

        }
    }
}
