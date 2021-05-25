using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel_Object : MonoBehaviour
{
    private GUI_PauseMenu EndOfLevel => FindObjectOfType<GUI_PauseMenu>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Cursor.visible = true;
            EndOfLevel.EndOfLevelMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
