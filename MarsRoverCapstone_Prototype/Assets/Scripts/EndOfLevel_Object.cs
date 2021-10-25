using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel_Object : MonoBehaviour
{
    private GUI_PauseMenu EndOfLevel => FindObjectOfType<GUI_PauseMenu>();

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && GM_Objectives.completedObjectives)
        {
            Cursor.visible = true;
            EndOfLevel.EndOfLevelMenu.SetActive(true);
            Time.timeScale = 0.1f;
        }
        else if(collision.gameObject.tag == "Player" && !GM_Objectives.completedObjectives)
        {
            Cursor.visible = true;
            EndOfLevel.IncompleteEndOfLevelMenu.SetActive(true);
            Time.timeScale = 0.1f;
        }
    }
}
