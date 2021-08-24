using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnTimer : MonoBehaviour
{
    public bool returnToStart = true;
    public float despawnTimer = 5f;
    private float _timer;
    private Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _timer = Time.time;

        if(returnToStart)
        {
            spawnPoint = transform.position;
        }
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
                if (Time.time >= (_timer + despawnTimer))
                {
                    Destroy(this.gameObject);
                }
                break;
        }

    }
}
