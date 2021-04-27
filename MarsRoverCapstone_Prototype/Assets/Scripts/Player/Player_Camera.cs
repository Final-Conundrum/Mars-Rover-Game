using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class Player_Camera : MonoBehaviour
{
    public GameObject player => FindObjectOfType<Player_Movement>().gameObject;

    public bool orbitCamera = true;
    public Vector3 cameraOffset = new Vector3(15f, 15f, 15f);

    // Look at Player options
    // Units above player the camera focuses on (set 0 to focus player in center of screen).
    public float lookAtOffset = 2f;

    // Mouse controls
    public float mouseSensitivity = 5f;

    public float minSmooth = 0.01f;
    public float maxSmooth = 0.2f;
    [Range(0.001f, 0.20f)] public float cameraSmooth = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Modify smooth values based on whether player is moving (lower number for movement, higher number for no movement)
        if(Input.GetAxis("Vertical") != 0)
        {
            cameraSmooth = minSmooth;
        }
        else
        {
            cameraSmooth = maxSmooth;
        }

        // Camera looking functions
        transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + lookAtOffset, player.transform.position.z));

        switch (orbitCamera)
        {
            case true:
                OrbitCamera();
                break;

            case false:
                LockedCamera();
                break;
        }
    }

    // Camera that orbits position of player via mouse controls
    private void OrbitCamera()
    {
        // Get mouse X input and calculate new position around player
        float rotateHorizontal = Input.GetAxis("Mouse X");

        Quaternion cameraAngle = Quaternion.AngleAxis(rotateHorizontal * mouseSensitivity, Vector3.up);

        cameraOffset = cameraAngle * cameraOffset;

        Vector3 cameraPosition = player.transform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, cameraPosition, cameraSmooth);
    }
       
    // Camera follows player in locked rotation behind them and rotates automatically with player movement
    private void LockedCamera()
    {
        transform.position = new Vector3(player.transform.position.x + cameraOffset.x, 
            player.transform.position.y + cameraOffset.y, 
            player.transform.position.z + cameraOffset.z);
    }
}
