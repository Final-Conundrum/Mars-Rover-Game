using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_PIXL_VirtualCursor : MonoBehaviour
{
    // Script added to the UI cursor object within the MiniGame_PIXL prefab
    MiniGame_PIXL parent => FindObjectOfType<MiniGame_PIXL>();

    public Vector3 StartPos;
    public float cursorSpeed = 1.5f;

    private void Start()
    {
        StartPos = parent.MazeStart.transform.position;
        transform.position = StartPos;
    }

    private void Update()
    {
        if(!GUI_PauseMenu.pausedGame)
        {
            transform.position += (new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * cursorSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MazeWall")
        {
            parent.Fail();
            transform.position = StartPos;

            Debug.Log("VirtualCursor: Hit Maze Wall");
        }

        if (collision.gameObject.tag == "MazeEnd")
        {
            parent.EditPIXLScreens(1);
        }
    }  
}
