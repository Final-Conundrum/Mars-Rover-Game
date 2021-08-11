using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI_PauseMenu : MonoBehaviour
{
    private GM_Checkpoint _GM_Checkpoint => FindObjectOfType<GM_Checkpoint>();

    public GameObject PauseMenu;
    public GameObject EndOfLevelMenu;
    public static bool pausedGame = false;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        EndOfLevelMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pausedGame && !GUI_DeathScreen.dead)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                pausedGame = true;
            }
            else if(pausedGame)
            {
                Cursor.visible = false;
                ResumeGame();
            
            }

        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;

        PauseMenu.SetActive(false);
        pausedGame = false;

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        _GM_Checkpoint.savedAtSafeZone = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
}
