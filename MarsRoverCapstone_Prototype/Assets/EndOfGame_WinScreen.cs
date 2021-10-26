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
    }

    public static void EndOfGame_Activate()
    {
        Cursor.visible = true;
        Time.timeScale = 0.1f;
        
    }
    
    public void GoToSequence(int newSequenceNum)
    {
        EndSequenceObjects[currentSequence].SetActive(false);

        EndSequenceObjects[newSequenceNum].SetActive(true);

        currentSequence = newSequenceNum;
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
