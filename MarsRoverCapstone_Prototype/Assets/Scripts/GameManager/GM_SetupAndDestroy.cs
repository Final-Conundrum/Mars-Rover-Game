using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_SetupAndDestroy : MonoBehaviour
{
    GameManager GM => FindObjectOfType<GameManager>();

    float waitToDestroy = 1f;

    // Update is called once per frame
    void Update()
    {
        GM.SceneSetup();
        Destroy(gameObject);
    }
}
