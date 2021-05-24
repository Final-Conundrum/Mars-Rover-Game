using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame_SHERLOC : MonoBehaviour
{
    //This is to load the Scene for the Mini Game for the SHERLOC
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == gameObject.tag)
            if (gameObject.tag == "Player")
                SceneManager.LoadScene("Test_MiniGame");
    }
}