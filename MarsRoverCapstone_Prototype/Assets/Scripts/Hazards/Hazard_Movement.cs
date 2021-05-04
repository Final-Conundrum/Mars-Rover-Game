using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Movement : MonoBehaviour
{
    /* Make the Hazard travel around a zone to emulate a 'dust storm' 
     * Rotate the object while moving it along a set path. 
     * 
     * 
     */

    public GameObject[] checkPoints;
    GameObject hazard;


    // Start is called before the first frame update
    void Start()
    {
        hazard = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //the 'zone' will be made up of multiple checkpoints. the hazard will move between these points at random.
        foreach( GameObject point in checkPoints)
        {
            
        }
    }
}
