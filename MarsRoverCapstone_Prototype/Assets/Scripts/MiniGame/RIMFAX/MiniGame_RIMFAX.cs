using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_RIMFAX : MonoBehaviour
{
    public AudioSource scanSFX;
    public AudioSource winSFX;

    // Variables for scan line gameplay
    [Space]
    [Header("RIMFAX Objects")]
    public GameObject gameplayObject;

    [Space]
    [Header("Scan line variables")]
    public GameObject scanline;
    public float scanlineSpeed;
    public float scallineMaxSpeed;

    public GameObject ScanPointA;
    public GameObject ScanPointB;
    private string currentTarget;

    public GameObject[] scanningLines;
    public int scanninglinesCounter = 0;

    [Space]
    [Header("UI Elements")]
    public GameObject failText;
    public float failTextTimer;

    // Images and panels for result info
    public Image[] HidingPanels;
    public GameObject IntroPanel;
    public GameObject[] TextPanel;

    private bool completed = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = "B";

        Cursor.visible = true;
        IntroPanel.SetActive(true);
        failText.SetActive(false);
        gameplayObject.SetActive(false);
        scanline.SetActive(false);

        // Sets randomized position of scan lines
        for (int i = 0; i < scanningLines.Length; i++)
        {
            scanningLines[i].transform.position = new Vector3(Random.Range(ScanPointA.transform.position.x, ScanPointB.transform.position.x), ScanPointA.transform.position.y, ScanPointA.transform.position.z);
        }

        foreach(GameObject i in TextPanel)
        {
            i.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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

                    // Increase speed
                    if (scanlineSpeed <= scallineMaxSpeed)
                    {
                        scanlineSpeed++;
                    }

                    // Sets randomized position of scan lines
                    for (int i = 0; i < scanningLines.Length; i++)
                    {
                        if (scanningLines[i] != null)
                        {
                            scanningLines[i].transform.position = new Vector3(Random.Range(ScanPointA.transform.position.x, ScanPointB.transform.position.x), ScanPointA.transform.position.y, ScanPointA.transform.position.z);
                        }
                    }
                    scanSFX.PlayOneShot(scanSFX.clip);
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
                    
                    // Increase speed
                    if (scanlineSpeed <= scallineMaxSpeed)
                    {
                        scanlineSpeed++;
                    }

                    if (!completed)
                    {
                        // Sets randomized position of scan lines
                        for (int i = 0; i < scanningLines.Length - 1; i++)
                        {
                            if (scanningLines[i] != null)
                            {
                                scanningLines[i].transform.position = new Vector3(Random.Range(ScanPointA.transform.position.x, ScanPointB.transform.position.x), ScanPointA.transform.position.y, ScanPointA.transform.position.z);
                            }
                        }
                    }
                    scanSFX.PlayOneShot(scanSFX.clip);
                }
                break;
        }

        // Detect win-condition
        if(scanninglinesCounter >= scanningLines.Length && !completed)
        {
            completed = true;
            EditRIMFAXScreens(2);          
        }
    }

    // Modify the appearance of the PIXL screens as player plays PIXL
    public void EditRIMFAXScreens(int variationNum)
    {
        switch (variationNum)
        {
            // Hide scan maze and display info about the analysis screen
            case 1:
                IntroPanel.SetActive(false);
                scanline.gameObject.SetActive(true);
                gameplayObject.SetActive(true);
                Cursor.visible = false;

                scanSFX.PlayOneShot(scanSFX.clip);
                break;
            case 2:
                scanline.gameObject.SetActive(false);
                Exit();
                break;
        }
    }

    // Fail-state warning: display text describing what the player did wrong.
    public IEnumerator Fail()
    {
        failText.SetActive(true);
        yield return new WaitForSeconds(failTextTimer);
        failText.SetActive(false);     
    }

    // End mini-game and display scan
    public void Exit()
    {
        completed = true;

        winSFX.PlayOneShot(winSFX.clip);

        Time.timeScale = 1;
        Cursor.visible = false;

        MiniGame_Systems.playingMinigame = false;
        Physical_Inventory.AddToInventory("RIMFAX");

        StartCoroutine(MiniGame_Results.ShowRIMFAXResults(5f));
        StartCoroutine(DestroyOnTimer(5f));
    }

    IEnumerator DestroyOnTimer(float timer)
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    // Called from buttons to reveal fact panels
    public void LearnButton(int panelNum)
    {
        TextPanel[panelNum].SetActive(true);
    }
}
