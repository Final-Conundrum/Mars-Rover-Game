using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Systems : MonoBehaviour
{
    // Attached Mini-game prefabs
    public GameObject GameObject_PIXL;
    public static GameObject Static_PIXL;

    // Start is called before the first frame update
    void Start()
    {
        // Assign static variables as prefabs
        Static_PIXL = GameObject_PIXL;
    }

    // Open the PIXL mini-game
    public void MiniGame_PIXL(){
        Instantiate(Static_PIXL);
    }

    public void MiniGame_RIMFAX()
    {

    }

    public void MiniGame_DRILL()
    {

    }
}
