using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Drill : MonoBehaviour
{
    // This script runs the drill minigame



    public static bool completed = false;
    public MiniGame_Drill ButtonActive;
    public float SetTime;

    public GameObject failText;
    public float failTextTimer;
    public float DrillTimer;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        StartMiniGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            failText.SetActive(false);
        }
    }


    // Play Mini-game
    public void StartMiniGame()
    {
        DrillTimer = SetTime;
        SetTime = 10f;
        
        completed = true;
    }
    // Failed
    public void FailMiniGame()
    {
        if SetTime => 10f;
        Destroy(this.gameObject);
        else{
            if SetTimer <= 2f;
            Destroy(this.gameObject);
        }
    }
    // End mini-game and display mineral analysis
    public void Exit()
    {
        Time.timeScale = 1;
        completed = true;
        Cursor.visible = true;
        GUI_MineralAnalysis.Display(true);

        Destroy(this.gameObject);
    }
}
