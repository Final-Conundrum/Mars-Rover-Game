using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MiniGame_Drill : MonoBehaviour
{
    // This script runs the drill minigame

    public static bool completed = false;
    public Button ButtonActive;
    public float DrillTime = 0f;
    public float WinTime = 10f;
    public float CoolDownTime = 2f;
    public GameObject failText;
    public float failTextTimer;
    private float timer;
    private float DrilltimeIteration;
 
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        completed = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            failText.SetActive(false);
        }

        {
            if (Time.time > DrilltimeIteration)
            {
                DrillTime++;
                DrilltimeIteration = Time.time + 1f;
            }
            {
                {
                    if (Time.time == CoolDownTime)
                        failText.SetActive(true);
                    {
                       CoolDown();
                    }
                }
            }
        }
        WinMiniGame();
    }


    // Win
    public void WinMiniGame()
    {
        if (DrillTime >= WinTime)
        {
            Exit();
        }
     
    }
    // CoolDown
   public void CoolDown()
    {
        CoolDownTime = Random.Range(0f, 10f);
        {
            if (Time.time == CoolDownTime)
            {
                DrillTime--;
                DrilltimeIteration = Time.time - 1f;
                failText.SetActive(true);
            }
        }
    }
    
    // End mini-game and display mineral analysis
    public void Exit()
    {
        Time.timeScale = 1;
        GUI_MineralAnalysis.Display(true);
        Destroy(this.gameObject);
    }
}
