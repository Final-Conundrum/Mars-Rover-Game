using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceUI_Trigger : MonoBehaviour
{
    public GameObject objectToAppear;

    // Start is called before the first frame update
    void Start()
    {
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
