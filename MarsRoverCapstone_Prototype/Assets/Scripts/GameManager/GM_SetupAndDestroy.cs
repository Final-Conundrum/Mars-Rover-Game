using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_SetupAndDestroy : MonoBehaviour
{
    GameManager GM => FindObjectOfType<GameManager>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GM.SceneSetup();
        Destroy(this.gameObject);
    }
}
