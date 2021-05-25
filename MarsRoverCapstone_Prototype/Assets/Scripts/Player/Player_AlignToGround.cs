using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AlignToGround : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * AlignToGround: 
     * Add this script to the child (Body) of the parent Player object so that it aligns the aesthetic Rover body to ground.
     */
    Player_Movement PM => GetComponentInParent<Player_Movement>();

    public float slopeRaycastDistance = 1.5f;
    public float sphereCastRadius = 1f;
    private RaycastHit _slopeCastHit;

    public Vector3 _CameraForward;

    public Transform rotateForward;
    public Transform rotateRight;
    public Transform rotateLeft;

    private void Update()
    {
        _CameraForward = PM.playerCam.transform.forward;

        // Correct rotation if it rotates too far

        if (PM.tankControls)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.parent.rotation.y, transform.rotation.z, transform.rotation.w);
        }
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);


        if (transform.rotation.x > 50f)
        {
            transform.rotation = new Quaternion(50f, transform.rotation.y, 0, transform.rotation.w);
        }
        if (transform.rotation.x > 50f)
        {
            transform.rotation = new Quaternion(-50f, transform.rotation.y, 0, transform.rotation.w);
        }

        if (transform.rotation.z > 50f)
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, 50f, transform.rotation.w);
        }

        if (transform.rotation.z > 50f)
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, -50f, transform.rotation.w);
        }

        // Finalize and rotate to Grounds Normal vector
        if (PM._alignToGround)
        {
            AlignToGround();
        }

        if (!PM.tankControls)
        {
            //Quaternion target = new Quaternion(0,0,0,0); 
            Transform target;

            if (Input.GetAxis("Vertical") > 0)
            {
                target = rotateForward;

                Vector3 lookAtPos = target.position - transform.position;
                Quaternion newRotation = Quaternion.LookRotation(lookAtPos, transform.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 3);
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                target = rotateLeft;
                Vector3 lookAtPos = target.position - transform.position;
                Quaternion newRotation = Quaternion.LookRotation(lookAtPos, transform.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);

            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                target = rotateRight;

                Vector3 lookAtPos = target.position - transform.position;
                Quaternion newRotation = Quaternion.LookRotation(lookAtPos, transform.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
            }
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

        if (Physics.SphereCast(raycast, sphereCastRadius, out hit, slopeRaycastDistance))
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + -transform.up * _slopeCastHit.distance, sphereCastRadius);
    }
}
