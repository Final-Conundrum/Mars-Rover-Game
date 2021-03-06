using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_Results : MonoBehaviour
{
    /* Mini Game Results Object:
     * This script contains coroutines that are called from the Minigame core scripts (MiniGame_RIMFAX, Minigame_PIXL, Minigame_Drill).
     * These coroutines triggers animations and randomizations of 'result screens' players view to learn a fun fact based on
     * the minigame they just completed.
     * 
     * Public variables are open to insert the various gameobjects and prefabs that comprise these result screens.
     */ 

    [Space]
    [Header("PIXL Result Assets")]
    public GameObject PIXL_ResultsObject;
    public static GameObject _PIXL_ResultsObject;

    public static Transform _PIXL_Transform;

    private static GameObject currentPIXLResult;

    private static bool PIXLActive = false;

    [Space]
    [Header("RIMFAX Result Assets")]
    public GameObject RIMFAX_ResultsObject;
    public static GameObject _RIMFAX_ResultsObject;

    public static Transform _RIMFAX_Transform;

    private static GameObject currentRIMFAXResult;

    private static bool RIMFAXActive = false;

    [Space]
    [Header("DRILL Result Assets")]
    public Image DRILL_ResultsObject;
    public static Image _DRILL_ResultsObject;
    public Sprite[] DRILL_ImagePrefab;
    public static Sprite[] _DRILL_ImagePrefab;

    public static Transform _DRILL_Transform;

    private static GameObject currentDRILLResult;

    private static bool DRILLActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        // Initialize variables
        PIXL_ResultsObject.gameObject.SetActive(false);
        RIMFAX_ResultsObject.gameObject.SetActive(false);
        DRILL_ResultsObject.gameObject.SetActive(false);

        _PIXL_ResultsObject = PIXL_ResultsObject;
        _PIXL_Transform = PIXL_ResultsObject.transform;

        _RIMFAX_ResultsObject = RIMFAX_ResultsObject;
        _RIMFAX_Transform = RIMFAX_ResultsObject.transform;

        _DRILL_ResultsObject = DRILL_ResultsObject;
        _DRILL_ImagePrefab = DRILL_ImagePrefab;
        _DRILL_Transform = DRILL_ResultsObject.transform;
    }

    private void Update()
    {
        // Check for input once a result screen has been triggered
        if(Input.GetKey(KeyCode.E) && PIXLActive)
        {
            StartCoroutine(HidePIXLResults());
        }

        if (Input.GetKey(KeyCode.E) && RIMFAXActive)
        {
            StartCoroutine(HideRIMFAXResults());
        }

        if (Input.GetKey(KeyCode.E) && DRILLActive)
        {
            StartCoroutine(HideDRILLResults());
        }
    }

    // Display and trigger animation of a random PIXL result screen
    public static IEnumerator ShowPIXLResults(float timer)
    {
        Debug.Log("MiniGame_Results: Showing PIXL results...");

        // Count viewed result images and reset if all viewed
        int viewcount = 0;
        foreach (GameObject i in GM_Objectives._PIXL_ImagePrefab)
        {
            if (i.GetComponent<MiniGame_ResultCheck>().hasBeenViewed)
            {
                viewcount++;
            }
        }

        if (viewcount == GM_Objectives._PIXL_ImagePrefab.Length)
        {
            foreach (GameObject i in GM_Objectives._PIXL_ImagePrefab)
            {
                i.GetComponent<MiniGame_ResultCheck>().hasBeenViewed = false;
            }
        }


        // Randomize, Display and Animate PIXL image
        int random = Random.Range(0, GM_Objectives._PIXL_ImagePrefab.Length - 1);

        while(GM_Objectives._PIXL_ImagePrefab[random].GetComponent<MiniGame_ResultCheck>().hasBeenViewed)
        {
            random = Random.Range(0, GM_Objectives._PIXL_ImagePrefab.Length);
        }

        GM_Objectives._PIXL_ImagePrefab[random].GetComponent<MiniGame_ResultCheck>().hasBeenViewed = true;

        _PIXL_ResultsObject.gameObject.SetActive(true);
        currentPIXLResult = Instantiate(GM_Objectives._PIXL_ImagePrefab[random], _PIXL_Transform);
        _PIXL_ResultsObject.GetComponent<Animator>().SetBool("Transition", true);

        PIXLActive = true;

        yield return new WaitForSeconds(timer);
    }

    // Hide the currently displayd PIXL result screen
    public IEnumerator HidePIXLResults()
    {
        Debug.Log("MiniGame_Results: Closing PIXL results...");

        PIXLActive = false;

        // Animate exit for PIXL image and destroy
        _PIXL_ResultsObject.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);

        _PIXL_ResultsObject.gameObject.SetActive(false);
        Destroy(currentPIXLResult);
    }

    // Display and trigger animation of a random RIMFAX result screen
    public static IEnumerator ShowRIMFAXResults(float timer)
    {
        Debug.Log("MiniGame_Results: Showing RIMFAX results...");

        // Count viewed result images and reset if all viewed
        int viewcount = 0;
        foreach(GameObject i in GM_Objectives._RIMFAX_ImagePrefab)
        {
            if(i.GetComponent<MiniGame_ResultCheck>().hasBeenViewed)
            {
                viewcount++;
            }
        }

        if (viewcount == GM_Objectives._RIMFAX_ImagePrefab.Length - 1)
        {
            foreach (GameObject i in GM_Objectives._RIMFAX_ImagePrefab)
            {
                i.GetComponent<MiniGame_ResultCheck>().hasBeenViewed = false;
            }
        }

        // Randomize, Display and Animate RIMFAX image
        int random = Random.Range(0, GM_Objectives._RIMFAX_ImagePrefab.Length - 1);

        while (GM_Objectives._RIMFAX_ImagePrefab[random].GetComponent<MiniGame_ResultCheck>().hasBeenViewed)
        {
            random = Random.Range(0, GM_Objectives._RIMFAX_ImagePrefab.Length - 1);
        }

        GM_Objectives._RIMFAX_ImagePrefab[random].GetComponent<MiniGame_ResultCheck>().hasBeenViewed = true;

        _RIMFAX_ResultsObject.gameObject.SetActive(true);
        currentRIMFAXResult = Instantiate(GM_Objectives._RIMFAX_ImagePrefab[random], _RIMFAX_Transform);
        _RIMFAX_ResultsObject.GetComponent<Animator>().SetBool("Transition", true);

        RIMFAXActive = true;

        yield return new WaitForSeconds(timer);
    }

    // Hide the currently displayd RIMFAX result screen
    public IEnumerator HideRIMFAXResults()
    {
        Debug.Log("MiniGame_Results: Closing RIMFAX results...");

        RIMFAXActive = false;

        // Animate exit for RIMFAX image and destroy
        _RIMFAX_ResultsObject.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);

        _RIMFAX_ResultsObject.gameObject.SetActive(false);
        Destroy(currentRIMFAXResult);
    }

    // Display and trigger animation of a random DRILL result screen
    public static IEnumerator ShowDRILLResults(float timer)
    {
        Debug.Log("MiniGame_Results: Showing DRILL results...");

        // Randomize, Display and Animate DRILL image
        int random = Random.Range(0, _DRILL_ImagePrefab.Length - 1);

        _DRILL_ResultsObject.gameObject.SetActive(true);
        _DRILL_ResultsObject.sprite = _DRILL_ImagePrefab[random];
        _DRILL_ResultsObject.GetComponent<Animator>().SetBool("Transition", true);

        DRILLActive = true;

        yield return new WaitForSeconds(timer);
    }

    // Hide the currently displayd DRILL result screen
    public IEnumerator HideDRILLResults()
    {
        Debug.Log("MiniGame_Results: Closing DRILL results...");

        DRILLActive = false;

        // Animate exit for DRILL image and destroy
        _DRILL_ResultsObject.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);

        _DRILL_ResultsObject.gameObject.SetActive(false);
        Destroy(currentDRILLResult);
    }
}
