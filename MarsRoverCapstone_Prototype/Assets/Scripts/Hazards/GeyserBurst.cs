using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBurst : MonoBehaviour
{
    public AudioSource audioSource => GetComponent<AudioSource>();

    public GameObject geyserBurst;

    // Position object
    public Transform posA;
    public Transform posB;

    public float burstSpeed = 5f;

    // Timers
    public float timerBetweenBurst = 5f;
    public float timerDuringBurst = 5f;
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
        if (Time.time > timerBetweenBurst)
        {
            StartCoroutine(GeyserBursting());
        }

        // Move position of geyser
        if (geyserBurst.transform.position != posB.position)
        {
            geyserBurst.transform.position = Vector3.MoveTowards(geyserBurst.transform.position, posB.position, burstSpeed * Time.deltaTime);
        }
    }

    private IEnumerator GeyserBursting()
    {
        geyserBurst.SetActive(true);

        // Transform position of geyser
        geyserBurst.transform.position = posA.position;

        GM_Audio.PlaySound(audioSource, "Geyser");

        // Set time for next burst
        timerBetweenBurst = Time.time + (_timerBetweenAdd + _timerDuringAdd);

        yield return new WaitForSeconds(timerDuringBurst);

        // Despawn geyser burst
        geyserBurst.SetActive(false);
    }
}
