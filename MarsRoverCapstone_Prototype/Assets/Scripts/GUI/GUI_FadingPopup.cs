using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_FadingPopup : MonoBehaviour
{
    private float timer;
    public TMP_Text text;

    public void Set(Vector3 pos, string message, float fadeTime)
    {
        text.transform.position = pos;
        text.text = message;
        Destroy(this.gameObject, fadeTime);
    }
}
