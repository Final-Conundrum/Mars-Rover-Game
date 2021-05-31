using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Time : MonoBehaviour
{
    public GameObject lightDAY;
    public Material skyboxDAY;

    public GameObject lightEVENING;
    public Material skyboxEVENING;

    public GameObject lightNIGHT;
    public Material skyboxNIGHT;


    // Start is called before the first frame update
    void Start()
    {
        SetSceneLights(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set lights in scene depending on given number associated with time of day
    public void SetSceneLights(float counter)
    {
        switch(counter)
        {
            case 1:
                lightDAY.SetActive(true);
                lightNIGHT.SetActive(false);
                RenderSettings.skybox = skyboxDAY;
                
                break;
            case 2:
                lightEVENING.SetActive(true);
                lightDAY.SetActive(false);
                RenderSettings.skybox = skyboxEVENING;

                break;
            case 3:
                lightNIGHT.SetActive(true);
                lightEVENING.SetActive(false);
                RenderSettings.skybox = skyboxNIGHT;

                break;
        }
    }
}
