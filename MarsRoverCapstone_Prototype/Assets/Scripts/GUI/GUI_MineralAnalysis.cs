using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class GUI_MineralAnalysis : MonoBehaviour
{
    public static string currentMineral = "";

    // Text and image elements on the Analysis screen
    public GameObject analysisScreen;
    public static GameObject static_analysisScreen;
    public TMP_Text _title;
    public TMP_Text _description;
    public string[] _descriptions;

    public Image _image;
    public Sprite[] _images;

    // Static variables
    public static TMP_Text title;
    public static TMP_Text description;
    public static Image image;

    public static string[] descriptions;
    public static Sprite[] images;

    // Start is called before the first frame update
    void Start()
    {
        static_analysisScreen = analysisScreen;
        static_analysisScreen.SetActive(false);

        title = _title;
        descriptions = _descriptions;
        description = _description;
        images = _images;
        image = _image;

    }

    // Display analysis and pause
    public static void Display(bool enabeled)
    {
        if(enabeled)
        {
            Time.timeScale = 0;
            GUI_PauseMenu.pausedGame = true;
            static_analysisScreen.SetActive(true);

            switch (currentMineral)
            {
                case "Random":
                    RandomMineral();
                    break;
                case "Feldspar":
                    description.text = descriptions[1];
                    image.sprite = images[1];
                    break;
                case "Aragonite":
                    description.text = descriptions[0];
                    image.sprite = images[0];
                    break;
            }

            title.text = "> Analysis > " + image.sprite.name;
        }
        else 
        {
            Time.timeScale = 1;
            GUI_PauseMenu.pausedGame = false;

            static_analysisScreen.SetActive(false);
        }
    }

    // Modify the analysis screen to display info on a random mineral
    public static void RandomMineral()
    {
        int num = Random.Range(0, descriptions.Length - 1);

        description.text = descriptions[num];
        image.sprite = images[num];
    }
}
