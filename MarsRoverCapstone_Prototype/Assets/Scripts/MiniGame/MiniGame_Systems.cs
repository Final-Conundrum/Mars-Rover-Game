using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Systems : MonoBehaviour
{
    public static bool playingMinigame = false;

    // Attached Mini-game prefabs
    public GameObject[] GameObject_PIXL;
    public static GameObject[] Static_PIXL;
    public GameObject GameObject_RIMFAX;
    public GameObject GameObject_DRILL;

    // Start is called before the first frame update
    void Start()
    {
        // Assign static variables as prefabs
        Static_PIXL = GameObject_PIXL;
    }

    // Open the PIXL mini-game
    public void MiniGame_PIXL(){

        int num = Random.Range(0, Static_PIXL.Length);

        Instantiate(Static_PIXL[num]);


        playingMinigame = true;
    }

    public void MiniGame_RIMFAX()
    {
        Instantiate(GameObject_RIMFAX);
        playingMinigame = true;

    }

    public void MiniGame_DRILL()
    {
        Instantiate(GameObject_DRILL);
        playingMinigame = true;
    }
}
