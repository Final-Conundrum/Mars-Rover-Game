using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroCameraDirector : MonoBehaviour
{
    public CinemachineVirtualCamera sceneCamera, goalsCamera, perseveranceCamera, instrumentCamera, minigameCamera, POICamera, hazardCamera, SZCamera, disclaimerCamera, finalCamera;
    public GameObject[] sceneProps, goalsProps, perseveranceProps, instrumentProps, minigameProps, POIProps, hazardProps, SZProps, disclaimerProps, finalProps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextCamera(int cameraPriority, CinemachineVirtualCamera camera, GameObject[] props)
    {
        cameraPriority++;

        foreach(GameObject i in props)
        {
            i.SetActive(true);
        }
    }
}
