using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame_WinScreen : MonoBehaviour
{
    public GameObject[] EndSequenceObjects;
    private int currentSequence = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject i in EndSequenceObjects)
        {
            i.SetActive(false);
        }

        EndSequenceObjects[0].SetActive(true);

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GM_Objectives.endOfGame)
        {
            gameObject.SetActive(true);
        }

    }

    public static void EndOfGame_Activate()
    {
        Cursor.visible = true;
        Time.timeScale = 1f;
    }
    
    public void GoToSequence(int newSequenceNum)
    {
        EndSequenceObjects[currentSequence].SetActive(false);
        Player_Expression.finaleCameras[currentSequence].Priority = 1;

        EndSequenceObjects[newSequenceNum].SetActive(true);
        Player_Expression.finaleCameras[newSequenceNum].Priority = 12;

        currentSequence = newSequenceNum;
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
