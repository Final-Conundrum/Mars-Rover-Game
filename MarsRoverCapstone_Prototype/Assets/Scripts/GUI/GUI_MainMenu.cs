using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_MainMenu : MonoBehaviour
{
    public GameObject DB;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        DB.SetActive(false);
        settingsMenu.SetActive(false);
        GM_SceneLoader.StartScene();
    }

    public void StartGame()
    {
        StartCoroutine(GM_SceneLoader.LoadToScene("IntroScene"));
    }

    public void Database(bool open)
    {
        DB.SetActive(open);
    }

    public void OpenSettings(bool open)
    {
        settingsMenu.SetActive(open);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
