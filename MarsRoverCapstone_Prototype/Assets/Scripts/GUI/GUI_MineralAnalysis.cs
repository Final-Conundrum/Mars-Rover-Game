using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_MineralAnalysis : MonoBehaviour
{
    public static string currentMineral = "";

    public GameObject analysisScreen;
    public static GameObject static_analysisScreen;
    public TMP_Text title;
    public TMP_Text description;
    public Image image;

    public string[] descriptions;
    public Sprite[] images;

    // Start is called before the first frame update
    void Start()
    {
        static_analysisScreen = analysisScreen;
        static_analysisScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        title.text = "> Analysis > " + currentMineral;

        switch (currentMineral) 
        {
            case "Feldspar":

                description.text = descriptions[0];
                image.sprite = images[0];
                break;

            case "Aragonite":
                description.text = descriptions[1];
                image.sprite = images[1];
                break;
        }
    }

    public static void Display(bool enabeled)
    {
        if(enabeled)
        {
            Time.timeScale = 0;
            GUI_PauseMenu.pausedGame = true;
            static_analysisScreen.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
            GUI_PauseMenu.pausedGame = false;

            static_analysisScreen.SetActive(false);
        }
    }
}
