using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_PIXL : MonoBehaviour
{
    // Script that runs the PIXL minigame.
    // VirtualCursor contains the MiniGame_PIXL_VirtualCursor script that is also required to function

    public static bool Completed = false;

    [Space]
    [Header("Audio sources")]
    public AudioSource SFX_Beep;
    public AudioSource SFX_Scan;

    // Associated game objects
    [Space]
    [Header("PIXL Game Objects")]
    public MiniGame_PIXL_VirtualCursor VirtualCursor;
    public GameObject MazeStart;
    public GameObject MazeEnd;

    // Images and panels for result info
    [Space]
    [Header("UI elements")]
    public Image[] HidingPanels;
    public GameObject IntroPanel;
    public GameObject[] TextPanels;

    public GameObject failText;
    public float failTextTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        IntroPanel.SetActive(true);
        VirtualCursor.gameObject.SetActive(false);

        foreach(GameObject i in TextPanels)
        {
            i.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Image i in HidingPanels)
        {
            i.color = new Color(0, 0, 0, Vector3.Distance(VirtualCursor.transform.position, MazeEnd.transform.position) / 100);
        }
    }

    // Play Mini-game
    public void StartMiniGame()
    {
        IntroPanel.SetActive(false);
        VirtualCursor.gameObject.SetActive(true);
        SFX_Beep.PlayOneShot(SFX_Beep.clip);

        foreach (Image i in HidingPanels)
        {
            i.gameObject.SetActive(true);
        }

        VirtualCursor.StartPos = Input.mousePosition - MazeStart.transform.position;

        Cursor.visible = false;
        Completed = false;

        failText.SetActive(false);

        SFX_Scan.Play();
    }

    // Fail-state warning: display text describing what the player did wrong.
    public IEnumerator Fail()
    {
        failText.SetActive(true);
        yield return new WaitForSeconds(failTextTimer);
        failText.SetActive(false);
    }

    // Modify the appearance of the PIXL screens as player plays PIXL
    public void EditPIXLScreens(int variationNum)
    {
        switch(variationNum)
        {
            // Hide intro and display maze
            case 1:
                StartMiniGame();
                break;
        }
    }

    // End mini-game and display mineral analysis
    public void Exit()
    {
        Time.timeScale = 1;
        Completed = true;
        Cursor.visible = true;

        GM_Objectives.UpdateObjective("PIXL");

        MiniGame_Systems.playingMinigame = false;

        StartCoroutine(MiniGame_Results.ShowPIXLResults(5f));
        StartCoroutine(DestroyOnTimer(5f));
    }

    IEnumerator DestroyOnTimer(float timer)
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    public void LearnButton(int panelNum)
    {
        TextPanels[panelNum].SetActive(true);
    }
}
