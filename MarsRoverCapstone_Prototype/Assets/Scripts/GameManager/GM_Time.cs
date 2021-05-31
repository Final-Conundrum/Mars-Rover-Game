using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Time : MonoBehaviour
{
    public GameObject lightDAY;
    public Cubemap skyboxDAY;

    public GameObject lightEVENING;
    public Cubemap skyboxEVENING;

    public GameObject lightNIGHT;
    public Cubemap skyboxNIGHT;


    // Start is called before the first frame update
    void Start()
    {
        
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

                
                break;
            case 2:
                lightEVENING.SetActive(true);
                lightDAY.SetActive(false);

                break;
            case 3:
                lightNIGHT.SetActive(true);
                lightEVENING.SetActive(false);

                break;
        }
    }
}
