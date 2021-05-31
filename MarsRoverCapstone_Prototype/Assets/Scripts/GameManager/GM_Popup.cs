using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GM_Popup : MonoBehaviour
{
    public TMP_Text popup;
    private float timeToFade;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToFade)
        {
            popup.gameObject.SetActive(false);
        }
    }

    public void FadingPopup(string message, float timer)
    {
        timeToFade = Time.time + timer;

        popup.text = message;
        popup.gameObject.SetActive(true);

        
    }
}
