using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_PIXL : MonoBehaviour
{
    // Script that runs the PIXL minigame.
    // VirtualCursor contains the MiniGame_PIXL_VirtualCursor script that is also required to function

    public static bool Completed = false;
    private AudioSource audioSource => GetComponent<AudioSource>();

    // Associated game objects
    public MiniGame_PIXL_VirtualCursor VirtualCursor;
    public GameObject MazeStart;
    public GameObject MazeEnd;

    // Images and panels for result info
    public Image[] HidingPanels;
    public GameObject ResultPanel;

    public GameObject failText;
    public float failTextTimer;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        StartMiniGame();
        ResultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            failText.SetActive(false);
        }

        foreach (Image i in HidingPanels)
        {
            i.color = new Color(0, 0, 0, Vector3.Distance(VirtualCursor.transform.position, MazeEnd.transform.position) / 100);
        }
    }

    // Play Mini-game
    public void StartMiniGame()
    {
        VirtualCursor.StartPos = Input.mousePosition - MazeStart.transform.position;

        Cursor.visible = false;
        Completed = false;

        failText.SetActive(false);
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

    // Modify the appearance of the PIXL screens as player plays PIXL
    public void EditPIXLScreens(int variationNum)
    {
        switch(variationNum)
        {
            // Hide scan maze and display info about the analysis screen
            case 1:
                GM_Audio.PlaySound(audioSource, "MGWin");

                ResultPanel.SetActive(true);
                VirtualCursor.gameObject.SetActive(false);
                Cursor.visible = true;

                foreach (Image i in HidingPanels)
                {
                    i.color = new Color(0, 0, 0, 1);
                }
                break;
        }
    }

    // End mini-game and display mineral analysis
    public void Exit()
    {
        GM_Audio.PlaySound(audioSource, "MGWin");

        Time.timeScale = 1;
        Completed = true;
        Cursor.visible = true;
        GUI_MineralAnalysis.Display(true);

        GM_Objectives.UpdateObjective("PIXL");

        MiniGame_Systems.playingMinigame = false;
        Destroy(this.gameObject);
    }
}
