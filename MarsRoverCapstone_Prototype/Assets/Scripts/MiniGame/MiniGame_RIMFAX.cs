using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_RIMFAX : MonoBehaviour
{
    // Variables for scan line
    public GameObject scanline;
    public float scanlineSpeed;

    public GameObject ScanPointA;
    public GameObject ScanPointB;
    private string currentTarget;


    public GameObject[] scanningLines;
    public int scanninglinesCounter = 0;

    // Images and panels for result info
    public Image[] HidingPanels;
    public GameObject ResultPanel;

    public GameObject failText;
    public float failTextTimer;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = "B";

        Cursor.visible = false;
        ResultPanel.SetActive(false);
        failText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Move scanline from two different points
        switch(currentTarget)
        {
            case "A":
                if(scanline.transform.position != ScanPointA.transform.position)
                {
                    scanline.transform.position = Vector3.MoveTowards(scanline.transform.position, ScanPointA.transform.position, scanlineSpeed);
                }
                else
                {
                    currentTarget = "B";
                }
                
                break;
            case "B":
                if (scanline.transform.position != ScanPointB.transform.position)
                {
                    scanline.transform.position = Vector3.MoveTowards(scanline.transform.position, ScanPointB.transform.position, scanlineSpeed);
                }
                else
                {
                    currentTarget = "A";
                }
                break;
        }

        // Detect win-condition
        if(scanninglinesCounter == scanningLines.Length)
        {
            EditRIMFAXScreens(1);
        }
    }

    // Modify the appearance of the PIXL screens as player plays PIXL
    public void EditRIMFAXScreens(int variationNum)
    {
        switch (variationNum)
        {
            // Hide scan maze and display info about the analysis screen
            case 1:
                ResultPanel.SetActive(true);
                scanline.gameObject.SetActive(false);
                Cursor.visible = true;

                foreach (Image i in HidingPanels)
                {
                    i.color = new Color(0, 0, 0, 1);
                }
                break;
        }
    }

    // Fail-state warning: display text describing what the player did wrong.
    public void Fail()
    {
        timer = Time.time + failTextTimer;

        failText.SetActive(true);

        if (Time.time > timer)
        {
            failText.SetActive(false);
        }
    }

    // End mini-game and display scan
    public void Exit()
    {
        Time.timeScale = 1;
        Cursor.visible = true;

        Destroy(this.gameObject);
    }
}
