using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBurst : MonoBehaviour
{
    public GameObject geyserBurst;

    // Position object
    public Transform posA;
    public Transform posB;

    public float burstSpeed = 5f;

    private bool bursting = false;

    // Timers
    public float timerBetweenBurst = 5f;
    public float timerDuringBurst = 3f;
    private float _timerBetweenAdd;
    private float _timerDuringAdd;

    // Start is called before the first frame update
    void Start()
    {
        _timerBetweenAdd = timerBetweenBurst;
        _timerDuringAdd = timerDuringBurst;

        geyserBurst.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn golf ball every timer check
        if (Time.timeSinceLevelLoad > timerBetweenBurst)
        {
            geyserBurst.SetActive(true);

            // Set time to despawn geyser burst
            timerDuringBurst = Time.timeSinceLevelLoad + _timerDuringAdd;

            // Set time for next burst
            timerBetweenBurst = Time.timeSinceLevelLoad + (_timerBetweenAdd + _timerDuringAdd);

            // Transform position of geyser
            geyserBurst.transform.position = posA.position;       
        }

        // Move position of geyser
        if (geyserBurst.transform.position != posB.position)
        {
            geyserBurst.transform.position = Vector3.MoveTowards(geyserBurst.transform.position, posB.position, burstSpeed * Time.deltaTime);
        }

        // Despawn geyser burst
        if (Time.timeSinceLevelLoad > timerDuringBurst)
        {
            geyserBurst.SetActive(false);

        }
    }
}
