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
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Exit()
    {
        Completed = true;
        Cursor.visible = false;
        Destroy(this.gameObject);
    }
}
