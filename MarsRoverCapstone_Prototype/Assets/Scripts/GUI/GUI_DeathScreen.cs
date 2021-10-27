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
    }

    public void Display()
    {
        Cursor.visible = true;
        DeathPanel.SetActive(true);
        dead = true;
        Time.timeScale = 1;
    }

    public void Reboot()
    {
        Physical_Inventory inventory = FindObjectOfType<Physical_Inventory>();
        inventory.ClearInventory();

        StartCoroutine(GM_SceneLoader.LoadToScene("Scene_MainGame"));
    }
}
