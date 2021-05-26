using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_PIXL : MonoBehaviour

{
    public static bool Completed = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Completed = false;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Exit()
    {
        Completed = true;
        Destroy(this.gameObject);
        GUI_MineralAnalysis.Display(true);
    }
}
