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
    public AudioSource audioSource => GetComponent<AudioSource>();

    public AudioClip _drivingSFX, _jumpSFX, _landingSFX, _injurySFX, _deathSFX, _MGWinSFX, _scanSFX, _geyserSFX;
    public static AudioClip drivingSFX, jumpSFX, landingSFX, injurySFX, deathSFX, MGWinSFX, scanSFX, geyserSFX;

    [Header("Background Audio")]
    public AudioSource _TTS_Message;
    public static AudioSource TTS_Message;
    public float TTS_OpeningTimer = 10f;

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
        geyserSFX = _geyserSFX;

        _TTS_Message = TTS_Message;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y) && !TTS_Message.isPlaying)
        {
            TTSSound("Opening", true);
        }
        else if(Input.GetKeyDown(KeyCode.Y) && TTS_Message.isPlaying)
        {
            TTSSound("Opening", false);
        }
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
            case "Geyser":
                audioSource.PlayOneShot(geyserSFX);
                break;
            case "Scan":
                audioSource.PlayOneShot(scanSFX);
                break;
        }
    }

    public static void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    // Play TTS sound file for tutorial/info
    public static void TTSSound(string TTSName, bool play)
    {
        switch (TTSName)
        {
            case "Opening":
                if(play)
                {
                    TTS_Message.Play();
                }
                else
                {
                    TTS_Message.Stop();
                }
                break;
        }
    }
}
