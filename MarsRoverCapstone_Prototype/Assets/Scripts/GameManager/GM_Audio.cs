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

    public AudioClip _drivingSFX, _jumpSFX, _landingSFX, _injurySFX, _deathSFX, _MGWinSFX, _scanSFX;
    public static AudioClip drivingSFX, jumpSFX, landingSFX, injurySFX, deathSFX, MGWinSFX, scanSFX;

    // Start is called before the first frame update
    void Start()
    {
        player = GM.player;
        jumpSFX = _jumpSFX;
        landingSFX = _landingSFX;
        injurySFX = _injurySFX;
        deathSFX = _deathSFX;
        MGWinSFX = _MGWinSFX;
        scanSFX = _scanSFX;
        drivingSFX = _drivingSFX;
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
            case "MGWin":
                audioSource.PlayOneShot(MGWinSFX);
                break;
            case "scanSFX":
                audioSource.PlayOneShot(scanSFX);
                break;
        }
    }

    public static void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }
}
