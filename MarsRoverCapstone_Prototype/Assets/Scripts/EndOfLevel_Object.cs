using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel_Object : MonoBehaviour
{
    private GUI_HUD EndOfLevelGUI => FindObjectOfType<GUI_HUD>();

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && GM_Objectives.completedObjectives)
        {
            EndOfLevelGUI.EndOfLevelMenu.SetActive(true);
            EndOfLevelGUI.transitionAnimation.SetBool("Transition", false);
            EndOfLevelGUI.EndOfLevelMenu.GetComponent<EndOfGame_WinScreen>().GoToSequence(0);
            EndOfGame_WinScreen.EndOfGame_Activate();
        }
        else if(collision.gameObject.tag == "Player" && !GM_Objectives.completedObjectives)
        {
            Cursor.visible = true;
            EndOfLevelGUI.IncompleteEndOfLevelMenu.SetActive(true);
            Time.timeScale = 0.1f;
        }
    }
}
