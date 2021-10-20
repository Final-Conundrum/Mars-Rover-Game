using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MiniGame_Drill : MonoBehaviour
{
    // UI Elements
    [Space]
    [Header("UI Elements")]
    public static bool completed = false;
    public Button ButtonActive;
    public Slider DrillSlider;
    public GameObject failText;
    public GameObject warningText;
    public GameObject drillImage;

    // Audio
    [Space]
    [Header("Audio Sources")]
    public AudioSource drillingSFX;
    public AudioSource beepSFX;

    // Variables for time
    [Space]
    [Header("Time variables for win-state and fail-state")]
    public float DrillTime = 0f;
    public float WinTime = 10f;

    public float CoolDownTime = 2f;
    [SerializeField] private float timeToCoolDown;
    private float OnClickTrigger;

    public float failTextTimer = 2f;
    private float DrilltimeIteration;

    private bool buttonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        completed = false;

        CoolDownTime = Time.time + Random.Range(2f, 9f);

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

        drillingSFX.pitch = DrillTime / 3;
    }


    // Win
    public void WinMiniGame()
    {
        drillingSFX.Stop();

        if (DrillTime >= WinTime && !completed)
        {
            Exit();
        }
        
    }
    // CoolDown
   public void CoolDown()
    {
        DrillTime -= 5;

        if(DrillTime < 0)
        {
            DrillTime = 0;
        }

        CoolDownTime = Random.Range(3f, 9f);

        StartCoroutine(Fail());
    }

    public IEnumerator Fail()
    {
        failText.SetActive(true);
        yield return new WaitForSeconds(failTextTimer);
        failText.SetActive(false);
    }

    // End mini-game and display mineral analysis
    public void Exit()
    {
        Time.timeScale = 1;
        GM_Objectives.UpdateObjective("Drill");
        MiniGame_Systems.playingMinigame = false;
        Physical_Inventory.AddToInventory("Drill");

        completed = true;

        StartCoroutine(MiniGame_Results.ShowDRILLResults(5f));
        StartCoroutine(DestroyOnTimer(5f));
    }

    IEnumerator DestroyOnTimer(float timer)
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    public void OnPress()
    {
        buttonDown = true;
        OnClickTrigger = Time.time;
        CoolDownTime = Random.Range(3f, 10f);

        drillImage.GetComponent<Animator>().SetBool("Drill", true);
        drillingSFX.Play();
    }

    public void OnRelease()
    {
        buttonDown = false;
        if(DrilltimeIteration >= OnClickTrigger + CoolDownTime)
        {
            DrillTime = 0;
        }

        drillImage.GetComponent<Animator>().SetBool("Drill", false);
        drillingSFX.Pause();
    }
}
