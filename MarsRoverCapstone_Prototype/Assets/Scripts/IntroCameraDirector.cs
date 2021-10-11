using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroCameraDirector : MonoBehaviour
{
    // Intro Director: Setup and activate different cameras and props in sequence to communicate the games goals and disclaimers
    // Organize priority of cameras and enable props for each part of sequence

    public CinemachineVirtualCamera[] sceneCameras;
    public GameObject[] sceneProps, goalsProps, perseveranceProps, SZProps, disclaimerProps, finalProps;
    private GameObject[][] propCollections;
    private int currentEvent = 0;

    // Start is called before the first frame update
    void Start()
    {
        propCollections = new GameObject[][] { sceneProps, goalsProps, perseveranceProps, SZProps, disclaimerProps, finalProps };

        // Activate first sequence objects
        foreach(GameObject i in propCollections[0])
        {
            i.SetActive(true);
        }

        sceneCameras[0].Priority = 11;
    }

    public void GoToCamera(int cameraPriority)
    {
        // Deactivate previous objects
        foreach(GameObject i in propCollections[currentEvent])
        {
            i.SetActive(false);
        }
        sceneCameras[currentEvent].Priority = 1;

        // Camera transition
        currentEvent = cameraPriority;
        sceneCameras[cameraPriority].Priority = 11;

        // Activate next objects
        foreach (GameObject i in propCollections[currentEvent])
        {
            i.SetActive(true);
        }

        Debug.Log("Intro Camera: Current camera priority = sceneCamera[" + cameraPriority + "]");
    }
}
