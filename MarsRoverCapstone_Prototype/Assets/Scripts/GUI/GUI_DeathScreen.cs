using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display()
    {
        Cursor.visible = true;
        DeathPanel.SetActive(true);
        dead = true;
    }
}