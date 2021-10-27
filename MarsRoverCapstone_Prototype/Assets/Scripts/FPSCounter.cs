using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TMP_Text FPSObject;
    
    // Start is called before the first frame update
    void Start()
    {
        FPSObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            FPSObject.gameObject.SetActive(true);
        }

        FPSObject.text = ((1f / Time.unscaledDeltaTime).ToString()) + " FPS";
    }
}
