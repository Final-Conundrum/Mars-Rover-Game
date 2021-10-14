using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_DeathScreen : MonoBehaviour
{
    public GameObject DeathPanel;
    public static bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        DeathPanel.SetActive(false);
        dead = false;
        Time.timeScale = 0;
    }

    public void Display()
    {
        Cursor.visible = true;
        DeathPanel.SetActive(true);
        dead = true;
    }

    public void Reboot()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
