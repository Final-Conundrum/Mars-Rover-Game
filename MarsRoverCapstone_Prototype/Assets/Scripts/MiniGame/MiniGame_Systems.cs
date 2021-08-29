using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Systems : MonoBehaviour
{
    public GameObject GameObject_PIXL;
    public static GameObject Static_PIXL;

    // Start is called before the first frame update
    void Start()
    {
        Static_PIXL = GameObject_PIXL;
    }

    public void MiniGame_PIXL(){
        Time.timeScale = 0;
        Instantiate(Static_PIXL);
    }
}
