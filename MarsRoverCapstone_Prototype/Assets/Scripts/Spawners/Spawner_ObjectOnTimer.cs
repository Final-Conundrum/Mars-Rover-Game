using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_ObjectOnTimer : MonoBehaviour
{
    public GameObject spawnObjectPrefab;
    private Vector3 spawnPoint;

    public float timer = 5f;
    private float timerAdd;

    // Start is called before the first frame update
    void Start()
    {
        timerAdd = timer;
        spawnPoint = new Vector3(0f, 1.62f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn golf ball every timer check
        if (Time.time > timer)
        {
            var newObj = Instantiate(spawnObjectPrefab) as GameObject;
            timer += timerAdd;

            newObj.transform.parent = gameObject.transform;
            newObj.transform.localPosition = spawnPoint;
        }
    }
}

