using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_PIXL : MonoBehaviour
{
    public static bool Completed = false;

    public MiniGame_PIXL_VirtualCursor VirtualCursor;
    private Collider2D VirtualCollider => VirtualCursor.GetComponent<Collider2D>();
    public GameObject MazeStart;
    public GameObject MazeEnd;
    
    public GameObject failText;

    // Start is called before the first frame update
    void Start()
    {

        StartMiniGame();
    }

    public void StartMiniGame()
    {
        VirtualCursor.StartPos = Input.mousePosition - MazeStart.transform.position;

        Cursor.visible = false;
        Completed = false;
    }

    public void Fail()
    {
        GameObject text = Instantiate(failText, new Vector3(0, -135f, 0), new Quaternion(0,0,0,0));
        Destroy(text, 2f);

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
