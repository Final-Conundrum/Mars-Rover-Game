using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_ObjectOnTimer : MonoBehaviour
{
    /* Edited by: Dallas
     * 
     * Spawner_ObjectOnTimer: Attach this script to an empty GameObject that will act as the spawn point.
     * Once a spawnObjectPrefab is assigned, the object will instantiate that object periodically.
     * 
     * For resource efficiency, returnToStart can be set true to return the spawned object back to its spawnpoint
     * instead of destroying it. 
     */

    public GameObject spawnObjectPrefab;
    private Vector3 spawnPoint;

    // Timers
    public float timer = 5f;
    private float globalTimer;
    private float _timerAdd;

    private void Awake()
    {
        globalTimer = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        _timerAdd = timer;

        spawnPoint = new Vector3(0f, 1.62f, 0f);

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn golf ball every timer check
        if (Time.timeSinceLevelLoad > timer )
        {
            GameObject newObj = Instantiate(spawnObjectPrefab);
            
            if(!spawnObjectPrefab.GetComponent<DespawnOnTimer>().returnToStart)
            {
                timer += _timerAdd;
            }

            newObj.transform.parent = gameObject.transform;
            newObj.transform.localPosition = spawnPoint;                    
        }
    }
}

