using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_SetupAndDestroy : MonoBehaviour
{
    GameManager GM => FindObjectOfType<GameManager>();

    // Update is called once per frame
    void Update()
    {
        GM.SceneSetup();
        Destroy(gameObject);
    }
}
