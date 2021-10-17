using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class IntroCameraDirector : MonoBehaviour
{
    // Intro Director: Setup and activate different cameras and props in sequence to communicate the games goals and disclaimers
    // Organize priority of cameras and enable props for each part of sequence

    public CinemachineVirtualCamera[] sceneCameras;
    public GameObject[] sceneProps, goalsProps, perseveranceProps, actionProps, SZProps, disclaimerProps, finalProps;
    private GameObject[][] propCollections;
    private int currentEvent = 0;

    public GameObject[] TextPanel;

    // Start is called before the first frame update
    void Start()
    {
        propCollections = new GameObject[][] { sceneProps, disclaimerProps, goalsProps, perseveranceProps, actionProps, SZProps, finalProps };

        // Deactivate all sequence objects
        foreach(CinemachineVirtualCamera i in sceneCameras)
        {
            i.Priority = 1;
        }

        foreach (GameObject[] x in propCollections)
        {
            foreach (GameObject i in x)
            {
                i.SetActive(false);
            }
        }

        foreach (GameObject i in TextPanel)
        {
            i.SetActive(false);
        }

        // Activate start sequence
        sceneCameras[0].Priority = 11;

        foreach (GameObject i in propCollections[0])
        {
            i.SetActive(true);
        }

    }

    public void GoToCamera(int cameraPriority)
    {
        // Deactivate previous objects
        foreach(GameObject i in propCollections[currentEvent])
        {
            i.SetActive(false);
        }

        foreach (GameObject i in TextPanel)
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

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

    public void LearnButton(int panelNum)
    {
        foreach (GameObject i in TextPanel)
        {
            i.SetActive(false);
        }

        TextPanel[panelNum].SetActive(true);
    }
}
