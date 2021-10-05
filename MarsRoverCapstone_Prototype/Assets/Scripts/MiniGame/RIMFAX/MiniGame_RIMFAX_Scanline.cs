using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_RIMFAX_Scanline : MonoBehaviour
{
    public MiniGame_RIMFAX parent;
    private Color colorStart;
    private Color colorOnTouch = Color.green;

    private void Start()
    {
        colorStart = GetComponent<Image>().color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "RIMFAX")
        {
            colorStart = other.gameObject.GetComponent<Image>().color;
            other.gameObject.GetComponent<Image>().color = colorOnTouch;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    { 
        if (other.gameObject.tag == "RIMFAX" && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Destroy(other.gameObject);
            parent.scanninglinesCounter++;
        }

        if (other.gameObject.tag != "RIMFAX" && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            parent.Fail();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "RIMFAX")
        {
            other.gameObject.GetComponent<Image>().color = colorStart;
        }
    }
}
