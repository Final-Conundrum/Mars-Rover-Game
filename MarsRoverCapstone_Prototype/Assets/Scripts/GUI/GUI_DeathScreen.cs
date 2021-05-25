using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_DeathScreen : MonoBehaviour
{
    public GameObject DeathPanel;

    // Start is called before the first frame update
    void Start()
    {
        DeathPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display()
    {
        Cursor.visible = true;
        DeathPanel.SetActive(true);
    }
}
