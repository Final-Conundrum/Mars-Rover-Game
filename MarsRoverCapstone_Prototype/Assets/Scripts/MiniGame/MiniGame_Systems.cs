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

    // Update is called once per frame
    void Update()
    {

    }
    public void MiniGame_PIXL(){
        Instantiate(Static_PIXL);
    }
}
