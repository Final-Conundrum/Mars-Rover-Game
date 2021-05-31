using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_PIXL : MonoBehaviour
{
    // Script that runs the PIXL minigame.
    // VirtualCursor contains the MiniGame_PIXL_VirtualCursor script that is also required to function

    public static bool Completed = false;

    public MiniGame_PIXL_VirtualCursor VirtualCursor;
    private Collider2D VirtualCollider => VirtualCursor.GetComponent<Collider2D>();
    public GameObject MazeStart;
    public GameObject MazeEnd;
    
    public GameObject failText;
    public float failTextTimer;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        StartMiniGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer)
        {
            failText.SetActive(false);
        }
    }

    public void StartMiniGame()
    {
        VirtualCursor.StartPos = Input.mousePosition - MazeStart.transform.position;

        Cursor.visible = false;
        Completed = false;

        failText.SetActive(false);
    }

    public void Fail()
    {
        timer = Time.time + failTextTimer;

        failText.SetActive(true);
    }



    public void Exit()
    {
        Completed = true;
        Cursor.visible = true;
        GUI_MineralAnalysis.Display(true);

        Destroy(this.gameObject);
    }
}
