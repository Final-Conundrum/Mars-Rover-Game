using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroCameraDirector : MonoBehaviour
{
    public CinemachineVirtualCamera sceneCamera, perseveranceCamera, instrumentCamera, minigameCamera, POICamera, hazardCamera, SZCamera, finalCamera;
    public GameObject[] sceneProps, perseveranceProps, instrumentProps, minigameProps, POIProps, hazardProps, SZProps, finalProps;

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


    }
}
