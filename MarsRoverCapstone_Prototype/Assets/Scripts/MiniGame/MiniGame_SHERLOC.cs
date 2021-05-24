using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame_SHERLOC : MonoBehaviour

{
    public static bool Completed = false;
    public void Exit()
    {
        Completed = true;
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
