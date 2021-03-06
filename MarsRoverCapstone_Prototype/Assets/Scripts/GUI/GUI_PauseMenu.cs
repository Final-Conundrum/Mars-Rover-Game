using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI_PauseMenu : MonoBehaviour
{
    private GM_Checkpoint _GM_Checkpoint => FindObjectOfType<GM_Checkpoint>();
    private GUI_HUD HUD => GetComponent<GUI_HUD>();

    public GameObject PauseMenu;
   
    public GameObject DataBaseMenu;
    public GameObject DataBaseReturnButton;
    public GameObject SettingsMenu;
    public static bool pausedGame = false;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        DataBaseMenu.SetActive(false);
        SettingsMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!pausedGame && !GUI_DeathScreen.dead)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
            HUD.playerHUD.SetActive(false);
            pausedGame = true;
        }
        else if (pausedGame)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;

        PauseMenu.SetActive(false);
        HUD.playerHUD.SetActive(true);

        pausedGame = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame(bool returnToCheckpoint)
    {
        _GM_Checkpoint.savedAtSafeZone = returnToCheckpoint;
        StartCoroutine(GM_SceneLoader.LoadToScene("Scene_MainGame"));
    }

    public void OpenSettings(bool open)
    {
        SettingsMenu.SetActive(open);
    }

    public void OpenDataBase(bool open)
    {
        DataBaseMenu.SetActive(open);
        DataBaseReturnButton.SetActive(open);
    }
}
