using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Setting_Audio : MonoBehaviour
{
    public AudioMixer mixer_SFX;
    public AudioMixer mixer_Music;

    // Attach to slider to set audio
    public void SetAudioLevel_SFX(float sliderValue)
    {
        
        mixer_SFX.SetFloat("ObjectVol", Mathf.Log10(sliderValue) * 20);
        
    }

    public void SetAudioLevel_Music(float sliderValue)
    {
        mixer_Music.SetFloat("BackgroundVol", Mathf.Log10(sliderValue) * 20);

    }
}
