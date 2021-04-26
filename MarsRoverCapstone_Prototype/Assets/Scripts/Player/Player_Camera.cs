using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public GameObject player => FindObjectOfType<Player_Movement>().gameObject;

    public bool orbitCamera = true;
    public Vector3 cameraOffset = new Vector3(15f, 15f, 15f);

    // Mouse controls
    public float mouseSensitivity = 5f;  
    [Range(0.01f, 1.0f)] public float cameraSmooth = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
      
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
       
    // Camera follows player in locked rotation
    private void LockedCamera()
    {
        transform.position = new Vector3(player.transform.position.x + cameraOffset.x, 
            player.transform.position.y + cameraOffset.y, 
            player.transform.position.z + cameraOffset.z);
    }
}
