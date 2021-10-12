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
    public Slider DrillSlider;
    public GameObject failText;
    public GameObject warningText;

    public float DrillTime = 0f;
    public float WinTime = 10f;

    public float CoolDownTime = 2f;
    [SerializeField] private float timeToCoolDown;
    private float OnClickTrigger;

    public float failTextTimer;
    private float timer;
    private float DrilltimeIteration;

    private bool buttonDown = false;
 
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        completed = false;

        CoolDownTime = Time.time + Random.Range(2f, 10f);

        warningText.SetActive(false);
        failText.SetActive(false);
        DrillSlider.maxValue = WinTime;

        timeToCoolDown = Time.time + CoolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Fail-state turned off
        if (Time.time > failTextTimer)
        {
            failText.SetActive(false);
        }

        // Playing state
        if (Time.time > DrilltimeIteration)
        {
            DrilltimeIteration = Time.time + 1f;

            if (buttonDown)
            {
                DrillTime++;
            }
        }

        // Warning and Fail-state
        if (buttonDown && (DrilltimeIteration >= OnClickTrigger + CoolDownTime - 1f))
        {
            warningText.SetActive(true);
        }
        if(DrilltimeIteration >= OnClickTrigger + CoolDownTime)
        {
            warningText.SetActive(false);

            if(buttonDown)
            {
                CoolDown();
            }
        }

        DrillSlider.value = DrillTime;
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
        DrillTime -= 5;
        failText.SetActive(true);
        failTextTimer = Time.time + 2f;

        CoolDownTime = Time.time + Random.Range(3f, 10f);
    }

    // End mini-game and display mineral analysis
    public void Exit()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
        GUI_MineralAnalysis.Display(true);

    }

    public void OnPress()
    {
        buttonDown = true;
        OnClickTrigger = Time.time;
        CoolDownTime = Time.time + Random.Range(3f, 10f);

    }

    public void OnRelease()
    {
        buttonDown = false;
        if(DrilltimeIteration >= OnClickTrigger + CoolDownTime)
        {
            DrillTime = 0;
        }
    }
}
