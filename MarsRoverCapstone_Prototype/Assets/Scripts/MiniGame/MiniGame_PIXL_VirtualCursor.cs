using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_PIXL_VirtualCursor : MonoBehaviour
{
    MiniGame_PIXL parent => FindObjectOfType<MiniGame_PIXL>();
    private bool userControl = false;



    private void Start()
    {
        transform.position = parent.MazeStart.transform.position;
        userControl = true;
    }


    private void Update()
    {
        if(userControl)
        {
            transform.position = parent.MazeStart.transform.position + Input.mousePosition;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MazeWall")
        {
            userControl = false;
            transform.position = Input.mousePosition - parent.MazeStart.transform.position;
            userControl = true;

            Debug.Log("VirtualCursor: Hit Maze Wall");
        }

        if (collision.gameObject.tag == "MazeEnd")
        {
            parent.Exit();
        }
    }  
}
