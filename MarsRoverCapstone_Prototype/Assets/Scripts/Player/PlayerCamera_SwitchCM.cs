using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera_SwitchCM : MonoBehaviour
{
    /* Swap Camera type whether player is playing with Tank controls enabled.
     * This allows for the camera to work more smoothly between two different playstyles.
     */
    Player_Movement PM => FindObjectOfType<Player_Movement>();
    public CinemachineFreeLook CameraTank;
    public CinemachineFreeLook CameraStandard;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TempColliderOff());
    }

    // Update is called once per frame
    void Update()
    {
        switch(PM.tankControls)
        {
            case true:
                CameraTank.Priority = 2;
                CameraStandard.Priority = 1;
                break;

            case false:
                CameraTank.Priority = 1;
                CameraStandard.Priority = 2;
                break;

        }
    }

    IEnumerator TempColliderOff()
    {
        CameraTank.GetComponent<CinemachineCollider>().m_AvoidObstacles = false;
        CameraStandard.GetComponent<CinemachineCollider>().m_AvoidObstacles = false;
        yield return new WaitForSeconds(5);
        CameraTank.GetComponent<CinemachineCollider>().m_AvoidObstacles = true;
        CameraStandard.GetComponent<CinemachineCollider>().m_AvoidObstacles = true;
    }
}
