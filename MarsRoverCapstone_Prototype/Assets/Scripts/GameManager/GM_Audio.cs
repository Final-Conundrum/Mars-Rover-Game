using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Audio : MonoBehaviour
{
    /* This script handles events that trigger SFX for the Rover to emit.
     * 
     * 
     */
    private GameManager GM => GetComponent<GameManager>();

    [Header("Player Rover Audio")]

    public GameObject player;
    public AudioSource playerSource;

    public AudioClip drivingSFX;
    public AudioClip drivingFastSFX;
    public AudioClip jumpSFX;
    public AudioClip landingSFX;
    public AudioClip injurySFX;
    public AudioClip deathSFX;

    // Start is called before the first frame update
    void Start()
    {
        player = GM.player;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") == 1)
        {
            
        }
    }
}
