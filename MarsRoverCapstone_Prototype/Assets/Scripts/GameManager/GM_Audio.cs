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

    public AudioClip _drivingSFX, _drivingFastSFX, _jumpSFX, _landingSFX, _injurySFX, _deathSFX;
    public static AudioClip drivingSFX, drivingFastSFX, jumpSFX, landingSFX, injurySFX, deathSFX;

    // Start is called before the first frame update
    void Start()
    {
        player = GM.player;
    }

    public static void PlaySound(AudioSource audioSource, string soundName)
    {
        switch(soundName)
        {
            case "Jump":
                audioSource.PlayOneShot(jumpSFX);
                break;
            case "Injury":
                audioSource.PlayOneShot(injurySFX);
                break;
            case "Landing":
                audioSource.PlayOneShot(landingSFX);
                break;
            case "Death":
                audioSource.PlayOneShot(deathSFX);
                break;
        }
    }
}
