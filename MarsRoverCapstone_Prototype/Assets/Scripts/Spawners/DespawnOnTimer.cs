using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnTimer : MonoBehaviour
{
    private GM_Checkpoint _GM_Checkpoint => FindObjectOfType<GM_Checkpoint>();

    public bool returnToStart = true;
    public bool ignoreIfRespawn = false;
    public float despawnTimer = 5f;
    private float _timer;
    private Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if(ignoreIfRespawn = true && _GM_Checkpoint.savedAtSafeZone)
        {
            gameObject.SetActive(false);
        }*/

        if(returnToStart)
        {
            spawnPoint = transform.position;
        }

        _timer = Time.timeSinceLevelLoad;

    }

    // Update is called once per frame
    void Update()
    {
        switch(returnToStart)
        {
            case true:
                if (Time.time >= (_timer + despawnTimer))
                {
                    transform.localPosition = spawnPoint;
                    _timer = Time.time + despawnTimer;
                }
                break;
            case false:
                if (Time.timeSinceLevelLoad >= (_timer + despawnTimer))
                {
                    Destroy(gameObject);
                }
                break;
        }

    }
}
