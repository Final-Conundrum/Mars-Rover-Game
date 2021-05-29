using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniGame_PIXL : MonoBehaviour

{
    public static bool Completed = false;

    public GameObject VirtualCursor;
    private Collider2D VirtualCollider => VirtualCursor.GetComponent<Collider2D>();
    public GameObject MazeStart;
    public GameObject MazeEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Completed = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Exit()
    {
        Completed = true;
        Cursor.visible = true;
        GUI_MineralAnalysis.Display(true);

        Destroy(this.gameObject);
    }


}
