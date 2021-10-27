using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WorldSpaceUI_Trigger : MonoBehaviour
{
    // The objects that will appear when in player radius
    public GameObject objectToAppear;

    // The objectToAppear component for LookAtConstraint
    public LookAtConstraint lookAtConstraint;

    // Start is called before the first frame update
    void Start()
    {
        // Set LookAtConstraint to player camera
        lookAtConstraint = objectToAppear.GetComponent<LookAtConstraint>();
        lookAtConstraint.AddSource(Player_ParentObject.cameraSource);

        objectToAppear.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            objectToAppear.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            objectToAppear.SetActive(false);
        }
    }
}
