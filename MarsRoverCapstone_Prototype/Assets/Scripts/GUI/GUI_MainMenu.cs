using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_MainMenu : MonoBehaviour
{
    public GameObject DB;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject sourcedAssetsMenu;
    public GameObject sourcedAssetsMenu2;

    // Start is called before the first frame update
    void Start()
    {
        DB.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        sourcedAssetsMenu.SetActive(false);
        sourcedAssetsMenu2.SetActive(false);
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

    public void OpenCredits(bool open)
    {
        creditsMenu.SetActive(open);
    }

    public void OpenSourcedAssets(bool open)
    {
        sourcedAssetsMenu.SetActive(open);
    }

    public void OpenSourcedAssets2(bool open)
    {
        sourcedAssetsMenu2.SetActive(open);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
