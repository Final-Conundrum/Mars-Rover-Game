using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Click : MonoBehaviour
{
    // This is to move from one scene to the next with button click

    public void ButtonMoveScene(string level)
    {
        SceneManager.LoadScene("Test_Sprint2Obstacles");
    }
}
