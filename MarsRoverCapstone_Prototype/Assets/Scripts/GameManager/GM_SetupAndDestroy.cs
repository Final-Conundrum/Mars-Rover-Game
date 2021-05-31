using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_SetupAndDestroy : MonoBehaviour
{
    GameManager GM => FindObjectOfType<GameManager>();

    private float timer = 1;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        GM.SceneSetup();
        Destroy(this.gameObject);
    }
}
